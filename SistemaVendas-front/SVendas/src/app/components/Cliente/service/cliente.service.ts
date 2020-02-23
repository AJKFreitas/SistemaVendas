import { Params } from './../../../shared/models/Params';
import { Injectable } from '@angular/core';
import { Cliente, ClienteVM } from '../model/Cliente';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
    private http: HttpClient,
    public router: Router) { }

    public form: FormGroup = new FormGroup({
      id: new FormControl(''),
      nome: new FormControl('', Validators.required),
      cpf: new FormControl('',  Validators.required),
      telefone: new FormControl('', Validators.required),
      endereco  : new FormControl('', Validators.required),
    });

  iserir(cliente: ClienteVM): Observable<any> {
    const api = `${this.endpoint}/cliente`;
    return this.http.post(api, cliente)
      .pipe(
        catchError(this.handleError)
      );
  }
  editar(cliente: Cliente): Observable<any> {
    const api = `${this.endpoint}/cliente`;
    return this.http.put(api, cliente)
      .pipe(
        catchError(this.handleError)
      );
  }
  deletar(cliente: Cliente) {
    const api = `${this.endpoint}/cliente/${cliente.id}`;
    return this.http.delete(api)
      .pipe(
        catchError(this.handleError)
      );
  }
  listar(params: Params): Observable<any> {
    const api = `${this.endpoint}/clientes/buscar-todos`;
    return this.http.post(api, params).pipe(
      map((res: Response) => {
        return res || [];
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
  public resetForm() {
    this.form.reset();
    this.form.setErrors = null;
  }
  initializeFormGroup() {
    this.form.setValue({
      id: '',
      nome: '',
      cpf: '',
      telefone: '',
      endereco: '',
    });
  }
}
