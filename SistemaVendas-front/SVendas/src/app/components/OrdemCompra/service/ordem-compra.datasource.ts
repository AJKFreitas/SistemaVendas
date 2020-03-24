import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { OrdemCompra } from '../models/OrdemCompra';
import { OrdemCompraService } from './ordem-compra.service';



export class OrdemCompraDataSource implements DataSource<OrdemCompra> {

    private ordemCompraSubject = new BehaviorSubject<OrdemCompra[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private ordemCompraService: OrdemCompraService) {

    }

    CarregarOrdens(filtro = '', ordenacao = 'asc', paginaAtual = 0, tamanhoDaPagina = 5, ordenarPor?) {

        this.loadingSubject.next(true);

        this.ordemCompraService.buscarOrdens(filtro, ordenacao,
            paginaAtual, tamanhoDaPagina, ordenarPor).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(ordemCompra => {
                this.ordemCompraSubject.next(ordemCompra.pagina);
                this.loadingLenthSubject.next(ordemCompra.pageData.totalCount);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<OrdemCompra[]> {
        console.log('Connecting data source');
        return this.ordemCompraSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.ordemCompraSubject.complete();
        this.loadingSubject.complete();
    }

}
