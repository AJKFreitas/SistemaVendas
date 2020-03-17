import { PedidoVendaDataSource } from './../service/pedido-venda.datasource';
import { PedidoVenda } from './../models/PedidoVenda';
import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Produto } from '../../Produto/model/Produto';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ProdutoDataSource } from '../../Produto/services/produto.datasource';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { ProdutoService } from '../../Produto/services/produto.service';
import { Router } from '@angular/router';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';
import { ClienteDataSource } from '../../Cliente/service/cliente.datasource';
import { PedidoVendaService } from '../service/pedido-venda.service';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { ItemPedidoVenda } from '../models/ItemPedidoVenda';

@Component({
  selector: 'app-listar-pedido-venda',
  templateUrl: './listar-pedido-venda.component.html',
  styleUrls: ['./listar-pedido-venda.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ListarPedidoVendaComponent implements OnInit, AfterViewInit {

  @ViewChild(MatTable, { static: false }) table: MatTable<PedidoVenda>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filtroTabela') filtroTabela: ElementRef;

  fonteDeDadosDeVendas: PedidoVendaDataSource;
  displayedColumns: string[] = ['cliente', 'dataVenda', 'valorTotal', 'action'];
  pedidos: PedidoVenda[] = [];
  expandedElement: PedidoVenda | null;

  pageSizeOptions: number[] = [5, 10, 25, 100];


  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    public service: PedidoVendaService,
    public route: Router
  ) {
  }

  ngOnInit(): void {
    this.fonteDeDadosDeVendas = new PedidoVendaDataSource(this.service);
    this.fonteDeDadosDeVendas.CarregarVendas();
  }
  ngAfterViewInit() {
    fromEvent(this.filtroTabela.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.CarregarVendas();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.CarregarVendas())
      )
      .subscribe();
  }
  CarregarVendas() {
    this.fonteDeDadosDeVendas.CarregarVendas(
      this.filtroTabela.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getPaginatorData(event) {
    this.fonteDeDadosDeVendas.CarregarVendas('', 'asc', event.pageIndex, event.pageSize);
  }

  openModal(acao, element) {}
  novoPedido() {
    this.route.navigate(['/pedido']);
  }

 
}
