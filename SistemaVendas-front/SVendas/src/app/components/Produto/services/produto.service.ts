import { Fornecedor } from './../../Fornecedor/model/Fornecedor';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router, Params } from '@angular/router';
import { Produto, ProdutoVM } from '../model/Produto';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';

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

  public form: FormGroup = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl('', Validators.required),
    descricao: new FormControl(''),
    valor: new FormControl('', Validators.required),
  });

  listarProdutoss(): Observable<any> {
    const api = `${this.endpoint}/produto`;
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
    this.form.reset();
    this.form.setErrors = null;
  }
  initializeFormGroup() {
    this.form.setValue({
      id: '',
      nome: '',
      descricao: '',
      valor: '',
    });
  }
  iserir(produto: ProdutoVM): Observable<any> {
    const api = `${this.endpoint}/produto`;
    return this.http.post(api, produto)
      .pipe(
        catchError(this.handleError)
      );
  }
  editar(produto: Produto): Observable<any> {
    const api = `${this.endpoint}/produto`;
    return this.http.put(api, produto)
      .pipe(
        catchError(this.handleError)
      );
  }
  deletar(produto: Produto) {
    const api = `${this.endpoint}/produto/${produto.id}`;
    return this.http.delete(api)
      .pipe(
        catchError(this.handleError)
      );
  }

  listar(params: Params): Observable<any> {
    const api = `${this.endpoint}/produto/buscar-todos`;
    return this.http.post(api, params).pipe(
      map((res: Response) => {
        return res || [];
      }),
      catchError(this.handleError)
    );
  }

  listarTodos(): Observable<any> {
    const api = `${this.endpoint}/produto`;
    return this.http.get(api, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || []
      }),
      catchError(this.handleError)
    );
  }

}
