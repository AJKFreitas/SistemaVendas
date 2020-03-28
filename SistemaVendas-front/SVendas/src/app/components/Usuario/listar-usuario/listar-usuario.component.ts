import { UsuarioService } from './../services/usuario.service';
import { Usuario, UsuarioVM} from './../../Auth/shared/models/User';
import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatTable} from '@angular/material/table';
import { MatPaginator} from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
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

  fonteDadosUsuarios: UsuarioDataSource;
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
    private toastSevice: MensagemPopUPService,
    public service: UsuarioService,

  ) { }

  ngOnInit(): void {
     this.fonteDadosUsuarios = new UsuarioDataSource(this.service);
     this.fonteDadosUsuarios.loadUsuarios();
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
    this.fonteDadosUsuarios.loadUsuarios(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.fonteDadosUsuarios.loadUsuarios('', 'asc', event.pageIndex, event.pageSize);
  }

  abrirModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(ModalComponent, dialogConfig);

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
      this.carregarUsuarios();
    });
  }

  adicionar(usuario: Usuario) {
    this.inserir(new UsuarioVM(usuario.nome, usuario.email, usuario.senha, usuario.role));

  }
  atualizar(usuario: Usuario) {
    this.editar(usuario);
  }

  remover(usuario: Usuario) {
      this.excluir(usuario);
  }

  inserir(usuario: UsuarioVM) {
    this.spinnerService.show();
    this.service.iserir(usuario).subscribe((res) => {
      this.toastSevice.Sucesso('Sucesso!', 'Usuario cadastrado com sucesso!');
      this.spinnerService.hide();
      this.carregarUsuarios();
    },
      err => {
        this.carregarUsuarios();
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar cadastar Usuario!');
      }
    );
  }
  editar(usuario: Usuario) {
    this.spinnerService.show();
    this.service.editar(usuario).subscribe((res) => {
        this.spinnerService.hide();
        this.toastSevice.Sucesso('Sucesso!', 'Usuario alterado com sucesso!');
        this.carregarUsuarios();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alterar  Usuario!');
      }
    );
  }
  excluir(usuario: Usuario) {
    this.spinnerService.show();
    this.service.excluir(usuario).subscribe((res) => {
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Usuario excluido com sucesso!');
      this.carregarUsuarios();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar excluido Usuario!');
      }
    );
  }

}
