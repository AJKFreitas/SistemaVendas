import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Usuario } from '../../Auth/shared/models/User';
import { FormGroup, FormControl, Validators } from '@angular/forms';

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

    form: FormGroup = new FormGroup({
      $key: new FormControl(null),
      id: new FormControl(''),
      nome: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.email, Validators.required]),
      senha: new FormControl('', Validators.required),
      role: new FormControl('', Validators.required),
    });

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
