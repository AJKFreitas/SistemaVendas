import { PedidoVenda } from './../models/PedidoVenda';
import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { PedidoVendaService } from './pedido-venda.service';



export class PedidoVendaDataSource implements DataSource<PedidoVenda> {

    private pedidoVendaSubject = new BehaviorSubject<PedidoVenda[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private pedidoVendaService: PedidoVendaService) {

    }

    CarregarVendas(filtro = '', ordenacao = 'asc', paginaAtual = 0, tamanhoDaPagina = 5, ordenarPor?) {

        this.loadingSubject.next(true);

        this.pedidoVendaService.buscarVendas(filtro, ordenacao,
            paginaAtual, tamanhoDaPagina, ordenarPor).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(pedidoVenda => {
                this.pedidoVendaSubject.next(pedidoVenda.pagina);
                this.loadingLenthSubject.next(pedidoVenda.pageData.totalCount);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<PedidoVenda[]> {
        console.log('Connecting data source');
        return this.pedidoVendaSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.pedidoVendaSubject.complete();
        this.loadingSubject.complete();
    }

}
