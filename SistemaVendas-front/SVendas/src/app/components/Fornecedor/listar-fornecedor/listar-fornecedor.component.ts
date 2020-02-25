import { Fornecedor, FornecedorVM } from './../model/Fornecedor';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { FornecedorDialogComponent } from '../modal/fornecedor-dialog/fornecedor-dialog.component';
import { Params } from 'src/app/shared/models/Params';

@Component({
  selector: 'app-listar-fornecedor',
  templateUrl: './listar-fornecedor.component.html',
  styleUrls: ['./listar-fornecedor.component.css']
})
export class ListarFornecedorComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }
  @ViewChild(MatTable, { static: true }) table: MatTable<Fornecedor>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;


  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    public service: FornecedorService,
  ) { }
  dataSource: MatTableDataSource<Fornecedor>;
  displayedColumns: string[] = ['nome', 'telefone', 'cnpj', 'action'];
  fornecedores: Fornecedor[] = [];


  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.fornecedores);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    length = this.fornecedores.length;
    this.listarFornecedores(new Params(10, 1));
    this.pageEvent = new PageEvent();
  }

  getPaginatorData(event) {
    this.listarFornecedores(new Params(event.pageSize, event.pageIndex || 1));
  }

  openModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(FornecedorDialogComponent, dialogConfig);

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
      this.listarFornecedores(new Params(10, 1));
    });
  }

  addRowData(fornecedor: Fornecedor) {
    this.dataSource.data.push({
      id: '',
      nome: fornecedor.nome,
      cnpj: fornecedor.cnpj,
      telefone: fornecedor.telefone,
      produtos: fornecedor.produtos
    });
    this.registerFornecedor(new FornecedorVM(fornecedor.nome, fornecedor.cnpj, fornecedor.telefone));
    this.table.renderRows();
  }
  updateRowData(fornecedor: Fornecedor) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === fornecedor.id) {
        value.nome = fornecedor.nome;
      }
      return true;
    });
    this.updateFornecedor(fornecedor);
  }
  deleteRowData(fornecedor: Fornecedor) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== fornecedor.id;
    });
    this.deleteFornecedor(fornecedor);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  buscaPaginada(pageSize, pageIndex) {
    this.listarFornecedores(new Params(pageSize, pageIndex));
  }


  listarFornecedores(params: Params) {
    this.spinnerService.show();
    this.service.listar(params).subscribe(res => {
      if (res.result) {
        this.fornecedores = res;
        this.dataSource.paginator = this.paginator;
      }
      this.fornecedores = res;
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      this.spinnerService.hide();
    }, err => {
      this.spinnerService.hide();
    });
  }

  registerFornecedor(fornecedor: FornecedorVM) {
    this.spinnerService.show();
    this.service.iserir(fornecedor).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Success('Sucesso!', 'Fornecedor cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Fornecedor cadastrado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.listarFornecedores(new Params(10, 1));
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Fornecedor!');
      }
    );
  }
  updateFornecedor(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.editar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Fornecedor alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.listarFornecedores(new Params(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Fornecedor alterado com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar alterado Fornecedor!');
      }
    );
  }
  deleteFornecedor(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.deletar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Fornecedor excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.listarFornecedores(new Params(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Fornecedor excluido com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Fornecedor!');
      }
    );

  }
}
