import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatSort } from '@angular/material/sort';
import { Produto, ProdutoVM } from '../model/Produto';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { ProdutoService } from '../services/produto.service';
import { Params } from 'src/app/shared/models/Params';
import { ProdutoDialogComponent } from '../modal/produto-dialog/produto-dialog.component';

@Component({
  selector: 'app-gestao-produtos',
  templateUrl: './gestao-produtos.component.html',
  styleUrls: ['./gestao-produtos.component.css']
})
export class GestaoProdutosComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }
  @ViewChild(MatTable, { static: true }) table: MatTable<Produto>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;

  constructor(public dialog: MatDialog,
              private spinnerService: NgxSpinnerService,
              private toastSevice: ToastService,
              public service: ProdutoService) { }

  dataSource: MatTableDataSource<Produto>;
  displayedColumns: string[] = ['nome', 'descricao', 'valor', 'codigo', 'action'];
  produtos: Produto[] = [];


  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.produtos);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    length = this.produtos.length;
    this.listarProdutos(new Params(10, 1));
    this.pageEvent = new PageEvent();
  }
  getPaginatorData(event) {
    this.listarProdutos(new Params(event.pageSize, event.pageIndex || 1));
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
      this.listarProdutos(new Params(10, 1));
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

  buscaPaginada(pageSize, pageIndex) {
    this.listarProdutos(new Params(pageSize, pageIndex));
  }


  listarProdutos(params: Params) {
    this.spinnerService.show();
    this.service.listar(params).subscribe(res => {
      if (res.result) {
        this.produtos = res;
        this.dataSource.paginator = this.paginator;
      }
      this.produtos = res;
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      this.spinnerService.hide();
    }, err => {
      this.spinnerService.hide();
    });
  }

  registerProduto(produto: ProdutoVM) {
    this.spinnerService.show();
    this.service.iserir(produto).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Success('Sucesso!', 'Produto cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Produto cadastrado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.listarProdutos(new Params(10, 1));
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Produto!');
      }
    );
  }
  updateProduto(produto: Produto) {
    this.spinnerService.show();
    this.service.editar(produto).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Produto alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.listarProdutos(new Params(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Produto alterado com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar alterado Produto!');
      }
    );
  }
  deleteProduto(produto: Produto) {
    this.spinnerService.show();
    this.service.deletar(produto).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Produto excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.listarProdutos(new Params(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Produto excluido com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Produto!');
      }
    );

  }
}
