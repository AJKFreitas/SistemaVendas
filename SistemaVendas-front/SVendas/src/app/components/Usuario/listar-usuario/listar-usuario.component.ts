import { UsuarioService } from './../services/usuario.service';
import { Usuario } from './../../Auth/shared/models/User';
import { DialogBoxComponent } from './../../Shared/dialog-box/dialog-box.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';

@Component({
  selector: 'app-listar-usuario',
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent implements OnInit {
  dataSource: MatTableDataSource<Usuario>;
  displayedColumns: string[] = [ 'nome', 'email', 'role', 'action'];
  role: string;
  roles: string[] = ['Admin', 'Funcionario', 'Fornecedor', 'Vendedor'];

  @ViewChild(MatTable, { static: false }) table: MatTable<any>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;


  usuarios: Usuario[] = [];
  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    public service: UsuarioService
  ) {}
  ngOnInit(): void {
    this.listarUsuarioss();
    this.dataSource = new MatTableDataSource(this.usuarios);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

 

  listarUsuarioss() {
    this.spinnerService.show();
    this.service.listar().subscribe(res => {
      if (res.result) {
        this.usuarios = res;
      }
      this.dataSource = new MatTableDataSource(res);
      this.usuarios = res;
      this.spinnerService.hide();
    });
  }
}
