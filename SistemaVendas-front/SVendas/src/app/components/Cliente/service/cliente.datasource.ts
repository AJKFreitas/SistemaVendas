import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Cliente } from '../model/Cliente';
import { ClienteService } from './cliente.service';



export class ClienteDataSource implements DataSource<Cliente> {

    private clientesSubject = new BehaviorSubject<Cliente[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private numeroTotalDeRegistrosSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public numeroTotalDeRegistros$ = this.numeroTotalDeRegistrosSubject.asObservable();

    constructor(private clienteService: ClienteService) {

    }

    CarregarClientes(filtro = '', ordenarcao = 'asc', paginaAtual = 0, tamanhoDaPagina = 5){

        this.loadingSubject.next(true);

        this.clienteService.buscarClientes(filtro, ordenarcao,
            paginaAtual, tamanhoDaPagina).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(clientes => {
                this.clientesSubject.next(clientes.data);
                this.numeroTotalDeRegistrosSubject.next(clientes.pageData.totalCount);
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
