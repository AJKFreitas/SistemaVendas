import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Produto } from '../model/Produto';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
     private http: HttpClient,
     public router: Router
    ) { }

    iserir(produto: Produto): Observable<any> {
      const api = `${this.endpoint}/produto`;
      return this.http.post(api, produto)
        .pipe(
          catchError(this.handleError)
        );
    }
  
    listar(): Observable<any> {
      const api = `${this.endpoint}/produto`;
      return this.http.get(api, { headers: this.headers }).pipe(
        map((res: Response) => {
          return res || []
        }),
        catchError(this.handleError)
      );
    }

    handleError(error: HttpErrorResponse) {
      let msg = '';
      if (error.error instanceof ErrorEvent) {
        // client-side error
        msg = error.error.message;
      } else {
        // server-side error
        msg = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
      return throwError(msg);
    }
}
