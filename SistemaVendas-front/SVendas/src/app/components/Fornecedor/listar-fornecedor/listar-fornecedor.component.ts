import { Fornecedor, FornecedorVM } from './../model/Fornecedor';
import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { FornecedorService } from '../services/fornecedor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { FornecedorDialogComponent } from '../modal/fornecedor-dialog/fornecedor-dialog.component';
import { Parametros } from 'src/app/shared/models/Params';
import { FornecedorDataSource } from '../services/fornecedor.datasource';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

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
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;
  @ViewChild('input') input: ElementRef;

  dataSourceF: FornecedorDataSource;
  dataSource: MatTableDataSource<Fornecedor>;
  displayedColumns: string[] = ['nome', 'telefone', 'cnpj', 'action'];
  fornecedores: Fornecedor[] = [];


  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;
  public length: number;
  public pageIndex: number;

  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    public service: FornecedorService,
  ) { }

  ngOnInit(): void {
    this.dataSourceF = new FornecedorDataSource(this.service);
    this.dataSourceF.loadFornecedores();
    this.dataSource = new MatTableDataSource(this.fornecedores);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    length = this.fornecedores.length;
    this.listarFornecedores(new Parametros(10, 1));
    this.pageEvent = new PageEvent();
  }

  ngAfterViewInit() {
    fromEvent(this.input.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.carregarFornecedores();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.carregarFornecedores())
      )
      .subscribe();
  }


  carregarFornecedores() {
    this.dataSourceF.loadFornecedores(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.dataSourceF.loadFornecedores('', 'asc', event.pageIndex, event.pageSize);
    this.listarFornecedores(new Parametros(event.pageSize, event.pageIndex || 1));
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
      this.carregarFornecedores();
      this.listarFornecedores(new Parametros(10, 1));
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
    this.listarFornecedores(new Parametros(pageSize, pageIndex));
  }


  listarFornecedores(params: Parametros) {
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
        this.toastSevice.Sucesso('Sucesso!', 'Fornecedor cadastrado com sucesso!');
        this.spinnerService.hide();
      }
      this.toastSevice.Sucesso('Sucesso!', 'Fornecedor cadastrado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.carregarFornecedores();
        this.listarFornecedores(new Parametros(10, 1));
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar cadastar Fornecedor!');
      }
    );
  }
  updateFornecedor(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.editar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Sucesso('Sucesso!', 'Fornecedor alterado com sucesso!');
        this.spinnerService.hide();
      }
      this.carregarFornecedores();
      this.listarFornecedores(new Parametros(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Fornecedor alterado com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar alterado Fornecedor!');
      }
    );
  }
  deleteFornecedor(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.service.deletar(fornecedor).subscribe((res) => {
      if (res) {
        this.toastSevice.Sucesso('Sucesso!', 'Fornecedor excluido com sucesso!');
        this.spinnerService.hide();
      }
      this.carregarFornecedores();
      this.listarFornecedores(new Parametros(10, 1));
      this.spinnerService.hide();
      this.toastSevice.Sucesso('Sucesso!', 'Fornecedor excluido com sucesso!');
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar excluido Fornecedor!');
      }
    );

  }
}
