import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { OrdemCompraVM, OrdemCompra } from '../models/OrdemCompra';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OrdemCompraService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
    private http: HttpClient,
    public router: Router
  ) { }

  iserir(pedidoVenda: OrdemCompraVM): Observable<any> {
    const api = `${this.endpoint}/OrdemCompra`;
    return this.http.post<OrdemCompraVM>(api, pedidoVenda)
      .pipe(
        catchError(this.handleError)
      );
  }
  editar(pedidoVenda: OrdemCompraVM): Observable<any> {
    const api = `${this.endpoint}/OrdemCompra`;
    return this.http.put(api, pedidoVenda)
      .pipe(
        catchError(this.handleError)
      );
  }

  excluir(pedido: OrdemCompra): Observable<any> {
    const api = `${this.endpoint}/OrdemCompra/${pedido.id}`;
    return this.http.delete(api)
      .pipe(
        catchError(this.handleError)
      );
  }

  buscarOrdens(filtro = '', ordenacao = 'asc', paginaAtual = 0, tamanhoDaPagina = 5, ordenarPor?): Observable<any> {
    const api = `${this.endpoint}/OrdemCompra/buscar-pagina`;
    return this.http.get(api, {
      params: new HttpParams()
        .set('filter', filtro)
        .set('sortOrder', ordenacao)
        .set('NumeroDaPaginaAtual', paginaAtual.toString())
        .set('TamanhoDaPagina', tamanhoDaPagina.toString())
        .set('OrdenarPor', ordenarPor)
    }).pipe(
      map(res => res)
    );
  }

  handleError(error: HttpErrorResponse) {
    let msg = '';
    if (error.error instanceof ErrorEvent) {
      msg = error.error.message;
    } else {
      msg = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(msg);
  }
}
