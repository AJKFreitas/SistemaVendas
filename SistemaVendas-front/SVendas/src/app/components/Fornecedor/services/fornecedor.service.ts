import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Fornecedor } from '../model/Fornecedor';
import { catchError, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
     private http: HttpClient,
     public router: Router
    ) { }

     // Sign-up
  iserir(fornecedor: Fornecedor): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
    return this.http.post(api, fornecedor)
      .pipe(
        catchError(this.handleError)
      );
  }

  listarFornecedores(): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
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
