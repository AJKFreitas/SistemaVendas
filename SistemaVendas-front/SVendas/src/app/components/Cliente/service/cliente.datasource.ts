import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Cliente } from '../model/Cliente';
import { ClienteService } from './cliente.service';



export class ClienteDataSource implements DataSource<Cliente> {

    private clientesSubject = new BehaviorSubject<Cliente[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private clienteService: ClienteService) {

    }

    loadClientes(filter = '', sortDirection = 'asc', pageIndex = 0, pageSize = 5){

        this.loadingSubject.next(true);

        this.clienteService.buscarClientes(filter, sortDirection,
            pageIndex, pageSize).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(clientes => {
                this.clientesSubject.next(clientes.data);
                this.loadingLenthSubject.next(clientes.pageData.totalCount);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<Cliente[]> {
        console.log('Connecting data source');
        return this.clientesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.clientesSubject.complete();
        this.loadingSubject.complete();
    }

}
