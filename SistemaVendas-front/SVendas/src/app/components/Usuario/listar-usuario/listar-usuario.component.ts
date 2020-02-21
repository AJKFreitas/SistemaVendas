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
  @ViewChild(MatTable, { static: false }) table: MatTable<any>;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  usuarios: Usuario[] = [];
  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    private usuarioService: UsuarioService
  ) {}
  ngOnInit(): void {
    this.listarUsuarioss();
    this.dataSource = new MatTableDataSource(this.usuarios);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
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

  addRowData(usuario: Usuario) {
    this.dataSource.data.push({
      id: usuario.id,
      nome: usuario.nome,
      email: usuario.email,
      senha: usuario.senha,
      role: usuario.role
    });

    this.table.renderRows();
  }
  updateRowData(usuario: Usuario) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === usuario.id) {
        value.nome = usuario.nome;
      }
      return true;
    });
  }
  deleteRowData(usuario: Usuario) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== usuario.id;
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  listarUsuarioss() {
    this.spinnerService.show();
    this.usuarioService.listar().subscribe(res => {
      if (res.result) {
        this.usuarios = res;
      }
      this.dataSource = new MatTableDataSource(res);
      this.usuarios = res;
      console.log(this.dataSource);
      this.spinnerService.hide();
    });
  }
}
