import { Fornecedor } from './../model/Fornecedor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { Usuario } from '../../Auth/shared/models/User';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DialogBoxComponent } from '../../Shared/dialog-box/dialog-box.component';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../Usuario/modal/modal.component';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { FornecedorDialogComponent } from '../modal/fornecedor-dialog/fornecedor-dialog.component';
import { Params } from 'src/app/shared/models/Params';

@Component({
  selector: 'app-listar-fornecedor',
  templateUrl: './listar-fornecedor.component.html',
  styleUrls: ['./listar-fornecedor.component.css']
})
export class ListarFornecedorComponent implements OnInit {
  fornecedores = [];
  columns = [];
  dataSource: MatTableDataSource<Fornecedor>;
  displayedColumns: string[] = ['nome', 'telefone', 'cnpj', 'action'];

  constructor(
    public dialog: MatDialog,
    public service: FornecedorService,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
  ) { }

  @ViewChild(MatTable, { static: true }) table: MatTable<Fornecedor>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;

  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;

  ngOnInit(): void {
    this.listarFornecedoresvoid();
  }

  listarFornecedoresvoid() {
    this.spinnerService.show();
    this.service.listarFornecedores().subscribe((res) => {
      if (res.result) {
        this.fornecedores = res;
      }
      this.fornecedores = res;
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      this.spinnerService.hide();
    });
  }

  getPaginatorData(event) {
    console.log(event);
    this.listarFornecedores(new Params(event.pageSize, event.pageIndex || 1));
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
    });
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
    });
  }

  addRowData(fornecedor: Fornecedor) {
    this.dataSource.data.push({
      id: fornecedor.id,
      nome: fornecedor.nome,
      cnpj: fornecedor.cnpj,
      telefone: fornecedor.telefone,
      produtos: fornecedor.produtos
    });

    this.table.renderRows();
  }
  updateRowData(fornecedor: Fornecedor) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === fornecedor.id) {
        value.nome = fornecedor.nome;
      }
      return true;
    });
  }
  deleteRowData(fornecedor: Fornecedor) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== fornecedor.id;
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  registerFornecedor(fornecedor: Fornecedor) {
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
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Fornecedor!');
      }
    );
  }
  updateUser(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.editar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Fornecedor alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Fornecedor alterado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar alterado Fornecedor!');
      }
    );
  }
  deleteUser(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.deletar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Fornecedor excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Fornecedor excluido com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Fornecedor!');
      }
    );

  }
}
