import { Parametros } from './../../../shared/models/Params';
import { Injectable } from '@angular/core';
import { Cliente, ClienteVM } from '../model/Cliente';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map } from 'rxjs/operators';
import { UntypedFormGroup, UntypedFormControl, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
    private http: HttpClient,
    public router: Router) { }

  public clienteFormGroup: UntypedFormGroup = new UntypedFormGroup({
    id: new UntypedFormControl(''),
    nome: new UntypedFormControl('', Validators.required),
    cpf: new UntypedFormControl('', Validators.required),
    telefone: new UntypedFormControl('', Validators.required),
    endereco: new UntypedFormControl('', Validators.required),
  });

  inserir(cliente: ClienteVM): Observable<any> {
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
  listar(params: Parametros): Observable<any> {
    const api = `${this.endpoint}/cliente/buscar-todos`;
    return this.http.post(api, params).pipe(
      map((res: Response) => {
        return res || [];
      }),
      catchError(this.handleError)
    );
  }

  listarClientes(): Observable<any> {
    const api = `${this.endpoint}/cliente`;
    return this.http.get(api, { headers: this.headers }).pipe(
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
    this.clienteFormGroup.reset();
    this.clienteFormGroup.setErrors = null;
  }
  initializeFormGroup() {
    this.clienteFormGroup.setValue({
      id: '',
      nome: '',
      cpf: '',
      telefone: '',
      endereco: '',
    });
  }

  buscarClientes(filter = '', sortOrder = 'asc',
                 pageNumber = 0, pageSize = 5): Observable<any> {
    const api = `${this.endpoint}/cliente/buscar-todos`;
    return this.http.get(api, {
      params: new HttpParams()
        .set('filter', filter)
        .set('sortOrder', sortOrder)
        .set('NumeroDaPaginaAtual', pageNumber.toString())
        .set('TamanhoDaPagina', pageSize.toString())
    }).pipe(
      map(res => res)
    );
  }
}
