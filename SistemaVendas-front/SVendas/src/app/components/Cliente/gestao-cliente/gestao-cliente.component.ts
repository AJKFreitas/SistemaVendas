import { Component, OnInit, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Action } from 'src/app/shared/modules/material/actionEnum';
import { MatDialogConfig, MatDialog } from '@angular/material/dialog';
import { ClienteDialogComponent } from '../modal/cliente-dialog/cliente-dialog.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
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
  @ViewChild('filtroTabela') filtroTabela: ElementRef;

  fonteDeDadosDeClientes: ClienteDataSource;
  cabecalhoTabelaClientes: string[] = ['nome', 'cpf', 'telefone', 'endereco', 'action'];

  tamanhosDePagina: number[] = [5, 10, 25, 100];

  constructor(
    public modalCliente: MatDialog,
    private servicoDeLoading: NgxSpinnerService,
    private mensagemPopUp: MensagemPopUPService,
    public service: ClienteService,
  ) { }

  ngOnInit(): void {
    this.fonteDeDadosDeClientes = new ClienteDataSource(this.service);
    this.fonteDeDadosDeClientes.CarregarClientes();
  }
  ngAfterViewInit() {
    fromEvent(this.filtroTabela.nativeElement, 'keyup')
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
    this.fonteDeDadosDeClientes.CarregarClientes(
      this.filtroTabela.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.fonteDeDadosDeClientes.CarregarClientes('', 'asc', event.pageIndex, event.pageSize);
  }

  abrirModalCliente(acao, objeto) {
    const configuracaoModalCliente = new MatDialogConfig();
    configuracaoModalCliente.disableClose = true;
    configuracaoModalCliente.autoFocus = true;
    configuracaoModalCliente.width = '60%';
    configuracaoModalCliente.data = objeto;
    objeto.action = acao;
    const respostaModalCliente = this.modalCliente.open(ClienteDialogComponent, configuracaoModalCliente);

    respostaModalCliente.afterClosed().subscribe(result => {
      if (result.event === Action.Adicionar) {
        this.adicionarCliente(result.data.value);
      } else if (result.event === Action.Editar) {
        this.editar(result.data.value);
      } else if (result.event === Action.Excluir) {
        this.excluir(result.data.value);
      } else {
        this.service.resetForm();
        this.service.initializeFormGroup();
        this.carregarClientes();
      }
    });
  }
  adicionarCliente(cliente: Cliente) {
    this.registrar(new ClienteVM(cliente.nome, cliente.cpf, cliente.telefone, cliente.endereco));
    this.table.renderRows();
    this.carregarClientes();
  }

  registrar(cliente: ClienteVM) {
    this.servicoDeLoading.show();
    this.service.iserir(cliente).subscribe((res) => {
      if (res.result) {
        this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente cadastrado com sucesso!');
        this.servicoDeLoading.hide();
      }
      this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente cadastrado com sucesso!');
      this.servicoDeLoading.hide();
      this.carregarClientes();
    },
      err => {
        this.servicoDeLoading.hide();
        this.mensagemPopUp.Erro('Erro ao tentar cadastar Usuario!');
      }
    );
  }

  editar(cliente: Cliente) {
    this.servicoDeLoading.show();
    this.service.editar(cliente).subscribe((res) => {
      if (res) {
        this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente alterado com sucesso!');
        this.servicoDeLoading.hide();
      }
      this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente alterado com sucesso!');
      this.servicoDeLoading.hide();
      this.carregarClientes();
    },
      err => {
        this.servicoDeLoading.hide();
        this.mensagemPopUp.Erro('Erro ao tentar alterado Usuario!');
      }
    );
  }

  excluir(cliente: Cliente) {
    this.servicoDeLoading.show();
    this.service.deletar(cliente).subscribe((res) => {
      if (res) {
        this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente excluido com sucesso!');
        this.servicoDeLoading.hide();
      }
      this.mensagemPopUp.Sucesso('Sucesso!', 'Cliente excluido com sucesso!');
      this.servicoDeLoading.hide();
      this.carregarClientes();
    },
      err => {
        this.servicoDeLoading.hide();
        this.mensagemPopUp.Erro('Erro ao tentar excluido Usuario!');
      }
    );

  }

}
