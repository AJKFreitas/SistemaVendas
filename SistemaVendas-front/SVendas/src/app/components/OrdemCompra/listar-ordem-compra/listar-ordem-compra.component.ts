import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatTable } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { MensagemPopUPService } from '../../Shared/ToastService';
import { Router } from '@angular/router';
import { OrdemCompra, OrdemCompraVM } from '../models/OrdemCompra';
import { OrdemCompraService } from '../service/ordem-compra.service';
import { OrdemCompraDataSource } from '../service/ordem-compra.datasource';
import { fromEvent, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, tap } from 'rxjs/operators';

@Component({
  selector: 'app-listar-ordem-compra',
  templateUrl: './listar-ordem-compra.component.html',
  styleUrls: ['./listar-ordem-compra.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ListarOrdemCompraComponent implements OnInit, AfterViewInit {

  @ViewChild(MatTable, { static: false }) table: MatTable<OrdemCompra>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filtroTabela') filtroTabela: ElementRef;

  fonteDeDadosDeOrdens: OrdemCompraDataSource;
  displayedColumns: string[] = ['fornecedor', 'dataEntrada', 'valorTotal', 'action'];
  ordems: OrdemCompra[] = [];
  expandedElement: OrdemCompra | null;

  pageSizeOptions: number[] = [5, 10, 25, 100];



  constructor(
    public dialog: MatDialog,
    private spinnerService: NgxSpinnerService,
    private toastSevice: MensagemPopUPService,
    public service: OrdemCompraService,
    public route: Router
  ) {
  }

  ngOnInit(): void {
    this.fonteDeDadosDeOrdens = new OrdemCompraDataSource(this.service);
    this.fonteDeDadosDeOrdens.CarregarOrdens();
  }
  ngAfterViewInit() {
    fromEvent(this.filtroTabela.nativeElement, 'keyup')
    .pipe(
        debounceTime(350),
        distinctUntilChanged(),
        tap(() => {
            this.paginator.pageIndex = 0;
            this.CarregarOrdens();
        })
    )
    .subscribe();

    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    merge(this.sort.sortChange, this.paginator.page)
          .pipe(
           tap(() => this.CarregarOrdens())
      )
      .subscribe();
  }
  CarregarOrdens() {
    console.log(this.sort);
    this.fonteDeDadosDeOrdens.CarregarOrdens(
      this.filtroTabela.nativeElement.value,
      this.sort.direction,
      this.paginator.pageIndex,
      this.paginator.pageSize,
      this.sort.active);
  }

  getPaginatorData(event) {
    console.log(event);
    this.fonteDeDadosDeOrdens.CarregarOrdens('', 'asc', event.pageIndex, event.pageSize);
  }

  openModal(acao, element) {}

  novaOrdem() {
    this.route.navigate(['/ordem']);
  }

 editar(ordemCompra: OrdemCompraVM ) {
  this.route.navigateByUrl('/ordem', {
    // tslint:disable-next-line:object-literal-shorthand
    state: { ordemCompra: ordemCompra }
    });
 }
  excluir(ordemCompra: OrdemCompra) {
    this.spinnerService.show();
    this.service.excluir(ordemCompra).subscribe((res) => {
      if (res) {
        this.toastSevice.Sucesso('Sucesso!', 'Ordem de Compra excluida com sucesso!');
        this.spinnerService.hide();
      }
      this.CarregarOrdens();
    },
      err => {
        this.spinnerService.hide();
        this.toastSevice.Erro('Erro ao tentar excluir Ordem de Compra!');
      }
    );

  }

}
