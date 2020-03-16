import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { PedidoVendaVM } from '../models/PedidoVenda';

@Injectable({
  providedIn: 'root'
})
export class PedidoVendaService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
    private http: HttpClient,
    public router: Router
  ) { }

  iserir(pedidoVenda: PedidoVendaVM): Observable<any> {
    const api = `${this.endpoint}/PedidoVenda`;
    return this.http.post(api, pedidoVenda)
      .pipe(
        catchError(this.handleError)
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

  buscarVendas(filtro = '', ordenacao = 'asc', paginaAtual = 0, tamanhoDaPagina = 5): Observable<any> {
    const api = `${this.endpoint}/pedidoVenda/buscar-pagina`;
    return this.http.get(api, {
      params: new HttpParams()
        .set('filter', filtro)
        .set('sortOrder', ordenacao)
        .set('NumeroDaPaginaAtual', paginaAtual.toString())
        .set('TamanhoDaPagina', tamanhoDaPagina.toString())
    }).pipe(
      map(res => res)
    );
  }
}
