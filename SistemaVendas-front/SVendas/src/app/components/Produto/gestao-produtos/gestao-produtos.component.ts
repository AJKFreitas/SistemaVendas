import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { MatPaginator} from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Produto, ProdutoVM } from '../model/Produto';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { ProdutoService } from '../services/produto.service';
import { ProdutoDialogComponent } from '../modal/produto-dialog/produto-dialog.component';
import { ProdutoDataSource } from '../services/produto.datasource';
import { tap, debounceTime, distinctUntilChanged} from 'rxjs/operators';
import { MatSort } from '@angular/material/sort';
import { merge, fromEvent } from 'rxjs';

@Component({
  selector: 'app-gestao-produtos',
  templateUrl: './gestao-produtos.component.html',
  styleUrls: ['./gestao-produtos.component.css']
})
export class GestaoProdutosComponent implements OnInit, AfterViewInit {

  @ViewChild(MatTable, { static: false }) table: MatTable<Produto>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  dataSourceP: ProdutoDataSource;
  dataSource: MatTableDataSource<Produto>;
  displayedColumns: string[] = ['nome', 'descricao', 'valor', 'action'];
  produtos: Produto[] = [];

  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize: number;
  public currentPage: number;
  public length: number;
  public pageIndex: number;

  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    public service: ProdutoService
  ) {
    this.dataSource = new MatTableDataSource(new Array<Produto>());
  }


  ngOnInit(): void {
    this.dataSourceP = new ProdutoDataSource(this.service);
    this.dataSourceP.loadProdutos();
  }


  ngAfterViewInit() {

    fromEvent(this.input.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.carregarProdutos();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.carregarProdutos())
      )
      .subscribe();
  }

  carregarProdutos() {
    this.dataSourceP.loadProdutos(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }
  getPaginatorData(event) {
    this.dataSourceP.loadProdutos('', 'asc', event.pageIndex, event.pageSize);
  }

  openModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(ProdutoDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result.event === Action.Adicionar) {
        this.addRowData(result.data.value);
      } else if (result.event === Action.Editar) {
        this.updateRowData(result.data.value);
      } else if (result.event === Action.Excluir) {
        this.deleteRowData(result.data.value);
      } else {
        this.service.resetForm();
        this.service.initializeFormGroup();
      }
      this.carregarProdutos();
    });
  }
  addRowData(produto: Produto) {
    this.dataSource.data.push({
      id: '',
      nome: produto.nome,
      descricao: produto.descricao,
      valor: produto.valor,
      codigo: produto.codigo,
      fornecedores: produto.fornecedores
    });
    this.registerProduto(new ProdutoVM(produto.nome, produto.descricao, produto.valor, produto.codigo, produto.fornecedores));

    this.table.renderRows();
  }
  updateRowData(produto: Produto) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === produto.id) {
        value.nome = produto.nome;
      }
      return true;
    });
    this.updateProduto(produto);
  }
  deleteRowData(produto: Produto) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== produto.id;
    });
    this.deleteProduto(produto);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  registerProduto(produto: ProdutoVM) {
    this.spinnerService.show();
    this.service.iserir(produto).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Sucesso('Sucesso!', 'Produto cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Sucesso('Sucesso!', 'Produto cadastrado com sucesso!');
      this.spinnerService.hide();
      this.carregarProdutos();
    },
      err => {
        this.carregarProdutos();
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar cadastar Produto!');
      }
    );
  }
  updateProduto(produto: Produto) {
    this.spinnerService.show();
    this.service.editar(produto).subscribe((res) => {
      if (res) {
        this.toastSevice.Sucesso('Sucesso!', 'Produto alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.carregarProdutos();
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Produto alterado com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alterado Produto!');
      }
    );
  }
  deleteProduto(produto: Produto) {
    this.spinnerService.show();
    this.service.deletar(produto).subscribe((res) => {
      if (res) {
        this.toastSevice.Sucesso('Sucesso!', 'Produto excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.carregarProdutos();
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Produto excluido com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar excluido Produto!');
      }
    );

  }

}

