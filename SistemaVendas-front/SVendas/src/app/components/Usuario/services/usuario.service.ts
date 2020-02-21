import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Usuario } from '../../Auth/shared/models/User';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
     private http: HttpClient,
     public router: Router
    ) { }

     // Sign-up
  iserir(usuario: Usuario): Observable<any> {
    const api = `${this.endpoint}/usuario`;
    return this.http.post(api, usuario)
      .pipe(
        catchError(this.handleError)
      );
  }

  listar(): Observable<any> {
    const api = `${this.endpoint}/usuario`;
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
