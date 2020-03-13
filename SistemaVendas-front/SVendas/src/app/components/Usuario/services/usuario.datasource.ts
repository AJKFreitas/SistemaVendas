import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { Observable, BehaviorSubject, of, pipe } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { Usuario } from '../../Auth/shared/models/User';
import { UsuarioService } from './usuario.service';



export class UsuarioDataSource implements DataSource<Usuario> {

    private usuariosSubject = new BehaviorSubject<Usuario[]>([]);

    private loadingSubject = new BehaviorSubject<boolean>(false);
    private loadingLenthSubject = new BehaviorSubject<number>(5);

    public loading$ = this.loadingSubject.asObservable();
    public lenth$ = this.loadingLenthSubject.asObservable();

    constructor(private usuarioService: UsuarioService) {

    }

    loadUsuarios(filter = '', sortDirection = 'asc', pageIndex = 0, pageSize = 5) {

        this.loadingSubject.next(true);

        this.usuarioService.buscarUsuarios(filter, sortDirection,
            pageIndex, pageSize).pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            )
            .subscribe(usuarios => {
                this.usuariosSubject.next(usuarios.data);
                this.loadingLenthSubject.next(usuarios.pageData.totalCount);
            });
    }

    connect(collectionViewer: CollectionViewer): Observable<Usuario[]> {
        console.log('Connecting data source');
        return this.usuariosSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.usuariosSubject.complete();
        this.loadingSubject.complete();
    }

}
