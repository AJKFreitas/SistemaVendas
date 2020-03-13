import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Fornecedor } from '../model/Fornecedor';
import { FornecedorService } from './fornecedor.service';



export class FornecedorDataSource implements DataSource<Fornecedor> {

    private fornecedoresSubject = new BehaviorSubject<Fornecedor[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private fornecedorService: FornecedorService) {

    }

    loadFornecedores(filter = '', sortDirection = 'asc', pageIndex = 0, pageSize = 5) {

        this.loadingSubject.next(true);

        this.fornecedorService.buscarFornecedores(filter, sortDirection,
            pageIndex, pageSize).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(fornecedores => {
                this.fornecedoresSubject.next(fornecedores.data);
                this.loadingLenthSubject.next(fornecedores.pageData.totalCount);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<Fornecedor[]> {
        console.log('Connecting data source');
        return this.fornecedoresSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.fornecedoresSubject.complete();
        this.loadingSubject.complete();
    }

}
