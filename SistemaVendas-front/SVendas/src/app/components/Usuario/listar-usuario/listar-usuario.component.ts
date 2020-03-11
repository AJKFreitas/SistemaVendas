import { UsuarioService } from './../services/usuario.service';
import { Usuario, UsuarioVM} from './../../Auth/shared/models/User';
import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTable} from '@angular/material/table';
import { MatPaginator} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { ModalComponent } from '../modal/modal.component';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { UsuarioDataSource } from '../services/usuario.datasource';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
  selector: 'app-listar-usuario',
  templateUrl: './listar-usuario.component.html',
  styleUrls: ['./listar-usuario.component.css']
})
export class ListarUsuarioComponent implements OnInit, AfterViewInit {
  @ViewChild(MatTable, { static: true }) table: MatTable<Usuario>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  // dataSource: MatTableDataSource<Usuario>;
  dataSourceU: UsuarioDataSource;
  displayedColumns: string[] = ['nome', 'email', 'role', 'action'];
  usuarios: Usuario[] = [];
  pageSizeOptions: number[] = [5, 10, 25, 100];
  public totalSize = 0;
  public pageSize: number;
  public currentPage: number;
  public length: number;
  public pageIndex: number;

  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    public service: UsuarioService,

  ) { }

  ngOnInit(): void {
     this.dataSourceU = new UsuarioDataSource(this.service);
     this.dataSourceU.loadUsuarios();
  }

  ngAfterViewInit() {
    fromEvent(this.input.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.carregarUsuarios();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.carregarUsuarios())
      )
      .subscribe();
  }

  carregarUsuarios() {
    this.dataSourceU.loadUsuarios(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.dataSourceU.loadUsuarios('', 'asc', event.pageIndex, event.pageSize);
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
      this.carregarUsuarios();
    });
  }

  addRowData(usuario: Usuario) {
    // this.dataSource.data.push({
    //   id: '',
    //   nome: usuario.nome,
    //   email: usuario.email,
    //   senha: usuario.senha,
    //   role: usuario.role
    // });
    this.registerUser(new UsuarioVM(usuario.nome, usuario.email, usuario.senha, usuario.role));
    this.table.renderRows();

  }
  updateRowData(usuario: Usuario) {
    // this.dataSource.data = this.dataSource.data.filter((value, key) => {
    //   if (value.id === usuario.id) {
    //     value.nome = usuario.nome;
    //   }
    //   return true;
    // });
    this.updateUser(usuario);
  }

  deleteRowData(usuario: Usuario) {
    // this.dataSource.data = this.dataSource.data.filter((value, key) => {
    //   return value.id !== usuario.id;
    // });
    this.deleteUser(usuario);
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
      this.carregarUsuarios();
    },
      err => {
        this.carregarUsuarios();
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
      this.carregarUsuarios();
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Usuario alterado com sucesso!');
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
      this.carregarUsuarios();
      this.spinnerService.hide();
      this.toastSevice.Success('Sucesso!', 'Usuario excluido com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Usuario!');
      }
    );
  }

}
