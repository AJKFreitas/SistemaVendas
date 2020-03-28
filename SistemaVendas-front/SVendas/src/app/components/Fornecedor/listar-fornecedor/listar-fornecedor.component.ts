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


  @ViewChild(MatTable, { static: true }) table: MatTable<Fornecedor>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(PageEvent) pageEvent: PageEvent;
  @ViewChild('input') input: ElementRef;

  fonteDadosFornecedor: FornecedorDataSource;
  displayedColumns: string[] = ['nome', 'telefone', 'cnpj', 'action'];
  fornecedores: Fornecedor[] = [];


  pageSizeOptions: number[] = [5, 10, 25, 100];
  public pageSize = 0;
  public currentPage = 0;
  public totalSize = 0;
  public length: number;
  public pageIndex: number;

  constructor(
    public modal: MatDialog,
    private spinnerService: NgxSpinnerService,
    private servicoDeMensagemPopUp: MensagemPopUPService,
    public fornecedorService: FornecedorService,
  ) { }

  ngOnInit(): void {
    this.fonteDadosFornecedor = new FornecedorDataSource(this.fornecedorService);
    this.fonteDadosFornecedor.loadFornecedores();
    length = this.fornecedores.length;
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
    this.fonteDadosFornecedor.loadFornecedores(
      this.input.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.fonteDadosFornecedor.loadFornecedores('', 'asc', event.pageIndex, event.pageSize);
  }

  openModal(action, obj) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = '40%';
    dialogConfig.data = obj;
    obj.action = action;
    const dialogRef = this.modal.open(FornecedorDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(result => {
      if (result.event === Action.Adicionar) {
        this.adicionar(result.data.value);
      } else if (result.event === Action.Editar) {
        this.atualizar(result.data.value);
      } else if (result.event === Action.Excluir) {
        this.remover(result.data.value);
      } else {
        this.fornecedorService.resetForm();
        this.fornecedorService.initializeFormGroup();
      }
      this.carregarFornecedores();
    });
  }

  adicionar(fornecedor: Fornecedor) {
    this.iserir(new FornecedorVM(fornecedor.nome, fornecedor.cnpj, fornecedor.telefone));
    this.carregarFornecedores();
  }
  atualizar(fornecedor: Fornecedor) {
    this.editar(fornecedor);
    this.carregarFornecedores();
  }
  remover(fornecedor: Fornecedor) {
    this.excluir(fornecedor);
    this.carregarFornecedores();
  }

  iserir(fornecedor: FornecedorVM) {
    this.spinnerService.show();
    this.fornecedorService.iserir(fornecedor).subscribe((res) => {
      this.spinnerService.hide();
      this.servicoDeMensagemPopUp.Sucesso('Sucesso!', 'Fornecedor cadastrado com sucesso!');
      this.spinnerService.hide();
    },
      err => {
        this.carregarFornecedores();
        this.spinnerService.hide();
        this.servicoDeMensagemPopUp.Erro('Erro ao tentar cadastar Fornecedor!');
        this.carregarFornecedores();
      }
    );
  }

  editar(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.fornecedorService.editar(fornecedor).subscribe((res) => {
      this.spinnerService.hide();
      this.servicoDeMensagemPopUp.Sucesso('Sucesso!', 'Fornecedor alterado com sucesso!');
      this.carregarFornecedores();
    },
      err => {
        this.spinnerService.hide();
        this.servicoDeMensagemPopUp.Erro('Erro ao tentar alterado Fornecedor!');
        this.carregarFornecedores();
      }
    );
  }
  excluir(fornecedor: Fornecedor) {
    this.spinnerService.show();
    this.fornecedorService.excluir(fornecedor).subscribe((res) => {
      this.spinnerService.hide();
      this.servicoDeMensagemPopUp.Sucesso('Sucesso!', 'Fornecedor excluido com sucesso!');
      this.carregarFornecedores();
    },
      err => {
        this.spinnerService.hide();
        this.servicoDeMensagemPopUp.Erro('Erro ao tentar excluido Fornecedor!');
        this.carregarFornecedores();
      }
    );

  }
}
