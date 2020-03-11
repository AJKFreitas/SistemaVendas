import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Produto } from '../model/Produto';
import { ProdutoService } from './produto.service';



export class ProdutoDataSource implements DataSource<Produto> {

    private produtosSubject = new BehaviorSubject<Produto[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private produtoService: ProdutoService) {

    }

    loadProdutos(filter = '', sortDirection = 'asc', pageIndex = 0, pageSize = 5){

        this.loadingSubject.next(true);

        this.produtoService.buscarProdutos(filter, sortDirection,
            pageIndex, pageSize).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(produtos => {
                this.produtosSubject.next(produtos.data);
                this.loadingLenthSubject.next(produtos.pageData.totalCount);
                console.log(this.lenth$);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<Produto[]> {
        console.log('Connecting data source');
        return this.produtosSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.produtosSubject.complete();
        this.loadingSubject.complete();
    }

}
