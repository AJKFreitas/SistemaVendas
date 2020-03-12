import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { ClienteDialogComponent } from '../modal/cliente-dialog/cliente-dialog.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastService } from '../../Shared/ToastService';
import { Cliente, ClienteVM } from '../model/Cliente';
import { ClienteService } from '../service/cliente.service';
import { ClienteDataSource } from '../service/cliente.datasource';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
  selector: 'app-gestao-cliente',
  templateUrl: './gestao-cliente.component.html',
  styleUrls: ['./gestao-cliente.component.css']
})
export class GestaoClienteComponent implements OnInit, AfterViewInit {

  @ViewChild(MatTable, { static: false }) table: MatTable<Cliente>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('input') input: ElementRef;

  dataSourceC: ClienteDataSource;
  dataSource: MatTableDataSource<Cliente>;
  displayedColumns: string[] = ['nome', 'cpf', 'telefone', 'endereco', 'action'];
  clientes: Cliente[] = [];

  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize: number;
  public currentPage: number;
  public length: number;
  public pageIndex: number;

  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: ToastService,
    public service: ClienteService,
  ) { }

  ngOnInit(): void {
    this.dataSourceC = new ClienteDataSource(this.service);
    this.dataSourceC.loadClientes();
  }
  ngAfterViewInit() {
    fromEvent(this.input.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.carregarClientes();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.carregarClientes())
      )
      .subscribe();
  }
  carregarClientes() {
    this.dataSourceC.loadClientes(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.dataSourceC.loadClientes('', 'asc', event.pageIndex, event.pageSize);
  }

  openModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '60%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.dialog.open(ClienteDialogComponent, dialogConfig);

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
        this.carregarClientes();
        // this.listarClientes(new PageParams(10, 1));
      }
    });
  }
  addRowData(cliente: Cliente) {
    this.dataSource.data.push({
      id: '',
      nome: cliente.nome,
      cpf: cliente.cpf,
      endereco: cliente.endereco,
      telefone: cliente.telefone
    });
    this.registerCliente(new ClienteVM(cliente.nome, cliente.cpf, cliente.telefone, cliente.endereco));
    this.table.renderRows();
    this.carregarClientes();
  }
  updateRowData(cliente: Cliente) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      if (value.id === cliente.id) {
        value.nome = cliente.nome;
      }
      return true;
    });
    this.updateCliente(cliente);
  }

  deleteRowData(cliente: Cliente) {
    this.dataSource.data = this.dataSource.data.filter((value, key) => {
      return value.id !== cliente.id;
    });
    this.deleteCliente(cliente);
  }
  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   this.dataSource.filter = filterValue.trim().toLowerCase();

  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }
  // buscaPaginada(pageSize, pageIndex) {
  //   this.listarClientes(new PageParams(pageSize, pageIndex));
  // }

  // listarClientes(params: PageParams) {
  //   this.spinnerService.show();
  //   this.service.listar(params).subscribe(res => {
  //     if (res.result) {
  //       this.clientes = res;
  //       this.dataSource.paginator = this.paginator;
  //     }
  //     this.clientes = res;
  //     this.dataSource = new MatTableDataSource(res);
  //     this.dataSource.paginator = this.paginator;
  //     this.spinnerService.hide();
  //   }, err =>{
  //     this.spinnerService.hide();
  //   });
  // }

  registerCliente(cliente: ClienteVM) {
    this.spinnerService.show();
    this.service.iserir(cliente).subscribe((res) => {
      if (res.result) {
        this.toastSevice.Success('Sucesso!', 'Cliente cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Cliente cadastrado com sucesso!');
      this.spinnerService.hide();
      this.carregarClientes();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar cadastar Usuario!');
      }
    );
  }

  updateCliente(cliente: Cliente) {
    this.spinnerService.show();
    this.service.editar(cliente).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Cliente alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Cliente alterado com sucesso!');
      this.spinnerService.hide();
      this.carregarClientes();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar alterado Usuario!');
      }
    );
  }

  deleteCliente(cliente: Cliente) {
    this.spinnerService.show();
    this.service.deletar(cliente).subscribe((res) => {
      if (res) {
        this.toastSevice.Success('Sucesso!', 'Cliente excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Success('Sucesso!', 'Cliente excluido com sucesso!');
      this.spinnerService.hide();
      this.carregarClientes();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Error('Erro ao tentar excluido Usuario!');
      }
    );

  }

}
