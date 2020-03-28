import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Produto, ProdutoVM } from '../model/Produto';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { ProdutoService } from '../services/produto.service';
import { ProdutoDialogComponent } from '../modal/produto-dialog/produto-dialog.component';
import { ProdutoDataSource } from '../services/produto.datasource';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
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

  fonteDadosProdutos: ProdutoDataSource;
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
  }


  ngOnInit(): void {
    this.fonteDadosProdutos = new ProdutoDataSource(this.service);
    this.fonteDadosProdutos.carregarProdutos();
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
    this.fonteDadosProdutos.carregarProdutos(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }
  getPaginatorData(event) {
    this.fonteDadosProdutos.carregarProdutos('', 'asc', event.pageIndex, event.pageSize);
  }

  abrirModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(ProdutoDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result.event === Action.Adicionar) {
        this.adicionar(result.data.value);
      } else if (result.event === Action.Editar) {
        this.atualizar(result.data.value);
      } else if (result.event === Action.Excluir) {
        this.remover(result.data.value);
      } else {
        this.service.resetForm();
        this.service.initializeFormGroup();
      }
      this.carregarProdutos();
    });
  }
  adicionar(produto: Produto) {
    this.inserir(new ProdutoVM(produto.nome, produto.descricao, produto.valor, produto.codigo, produto.fornecedores));
  }
  atualizar(produto: Produto) {
    this.editar(produto);
  }
  remover(produto: Produto) {
    this.excluir(produto);
  }



  inserir(produto: ProdutoVM) {
    this.spinnerService.show();
    this.service.iserir(produto).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Produto cadastrado com sucesso!');
      this.carregarProdutos();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar cadastar Produto!');
        this.carregarProdutos();
      }
    );
  }
  editar(produto: Produto) {
    this.spinnerService.show();
    this.service.editar(produto).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Produto alterado com sucesso!');
      this.carregarProdutos();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alterado Produto!');
      }
    );
  }
  excluir(produto: Produto) {
    this.spinnerService.show();
    this.service.deletar(produto).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Produto excluido com sucesso!');
      this.carregarProdutos();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar excluido Produto!');
      }
    );

  }

}

