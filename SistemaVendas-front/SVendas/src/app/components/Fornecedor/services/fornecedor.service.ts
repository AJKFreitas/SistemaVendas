import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router, Params } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Fornecedor, FornecedorVM } from '../model/Fornecedor';
import { catchError, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';

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

  public form: FormGroup = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl('', Validators.required),
    telefone: new FormControl(''),
    cnpj: new FormControl('', Validators.required),
  });

  // grid
  listarFornecedores(): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
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
      msg = error.error.message;
    } else {
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
      telefone: '',
      cnpj: null,
    });
  }

  iserir(fornecedor: FornecedorVM): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
    return this.http.post(api, fornecedor)
      .pipe(
        catchError(this.handleError)
      );
  }


  editar(fornecedor: Fornecedor): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
    return this.http.put(api, fornecedor)
      .pipe(
        catchError(this.handleError)
      );
  }
  excluir(fornecedor: Fornecedor) {
    const api = `${this.endpoint}/fornecedor/${fornecedor.id}`;
    return this.http.delete(api)
      .pipe(
        catchError(this.handleError)
      );
  }

  listar(params: Params): Observable<any> {
    const api = `${this.endpoint}/fornecedor/buscar-todos`;
    return this.http.post(api, params).pipe(
      map((res: Response) => {
        return res || [];
      }),
      catchError(this.handleError)
    );
  }

  buscarFornecedores(filter = '', sortOrder = 'asc', pageNumber = 0, pageSize = 5): Observable<any> {
    const api = `${this.endpoint}/fornecedor/buscar-todos`;
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
