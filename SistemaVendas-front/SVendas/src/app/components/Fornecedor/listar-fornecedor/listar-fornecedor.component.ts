import { Fornecedor } from './../model/Fornecedor';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { Usuario } from '../../Auth/shared/models/User';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DialogBoxComponent } from '../../Shared/dialog-box/dialog-box.component';

@Component({
  selector: 'app-listar-fornecedor',
  templateUrl: './listar-fornecedor.component.html',
  styleUrls: ['./listar-fornecedor.component.css']
})
export class ListarFornecedorComponent implements OnInit {
  fornecedores = [];
  columns = [];
  dataSource: MatTableDataSource<Fornecedor>;
  displayedColumns: string[] = [ 'nome', 'telefone', 'cnpj', 'action'];
  @ViewChild(MatTable, { static: false }) table: MatTable<any>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  dialog: any;
  constructor(
    public fornecedorService: FornecedorService,
    private SpinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    ){}


  ngOnInit(): void {
    this.listarFornecedores();
  }

  listarFornecedores() {
    this.SpinnerService.show();
    this.fornecedorService.listarFornecedores().subscribe((res) => {
      if (res.result) {
        this.fornecedores = res;
      }
      this.fornecedores = res;
      this.SpinnerService.hide();
    });
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '550px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event === 'Add') {
        this.addRowData(result.data);
      } else if (result.event === 'Update') {
        this.updateRowData(result.data);
      } else if (result.event === 'Delete') {
        this.deleteRowData(result.data);
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

}
