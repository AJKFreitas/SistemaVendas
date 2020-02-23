import { UsuarioService } from './../services/usuario.service';
import { Usuario, UsuarioVM } from './../../Auth/shared/models/User';
import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { ModalComponent } from '../modal/modal.component';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { Params } from 'src/app/shared/models/Params';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-listar-usuario',
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent implements OnInit, AfterViewInit {

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.dataSource.paginator = this.paginator;
  }


  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    public service: UsuarioService,

  ) { }
  dataSource: MatTableDataSource<Usuario>;
  displayedColumns: string[] = ['nome', 'email', 'role', 'action'];
  usuarios: Usuario[] = [];

  @ViewChild(MatTable, { static: true }) table: MatTable<Usuario>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;
 
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;



  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.usuarios);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    length = this.deleteUser.length;
    this.listarUsuarioss(new Params(10, 1));
    this.pageEvent = new PageEvent();
  }



  getPaginatorData(event){
    console.log(event);
    this.listarUsuarioss( new Params(event.pageSize, event.pageIndex || 1));
}




  openModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(ModalComponent, dialogConfig);

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


  addRowData(usuario: Usuario) {
    this.dataSource.data.push({
      id: '',
      nome: usuario.nome,
      email: usuario.email,
      senha: usuario.senha,
      role: usuario.role
    });
    this.registerUser(new UsuarioVM(usuario.nome, usuario.email, usuario.senha, usuario.role));
    this.table.renderRows();

  }
  updateRowData(usuario: Usuario) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === usuario.id) {
        value.nome = usuario.nome;
      }
      return true;
    });
    this.updateUser(usuario);
  }

  deleteRowData(usuario: Usuario) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== usuario.id;
    });
    this.deleteUser(usuario);
  }

  applyFilter(event: Event) {
    console.log('applyFilter', event);
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  buscaPaginada(pageSize, pageIndex) {
    this.listarUsuarioss(new Params(pageSize, pageIndex));
  }

  listarUsuarioss(params: Params) {
    this.spinnerService.show();
    this.service.listar(params).subscribe(res => {
      if (res.result) {
        this.usuarios = res;
      }
      this.dataSource = new MatTableDataSource(res);
      this.usuarios = res;
      console.log(this.dataSource);
      this.spinnerService.hide();
    });
  }

  registerUser(usuario: UsuarioVM) {
    this.spinnerService.show();
    this.service.iserir(usuario).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Success('Sucesso!', 'Usuario cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Usuario cadastrado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Usuario!');
      }
    );
  }
  updateUser(usuario: Usuario) {
    this.spinnerService.show();
    this.service.editar(usuario).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Usuario alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Usuario alterado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar alterado Usuario!');
      }
    );
  }
  deleteUser(usuario: Usuario) {
    this.spinnerService.show();
    this.service.deletar(usuario).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Usuario excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Usuario excluido com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Usuario!');
      }
    );

  }

 }
