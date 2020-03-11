import { Fornecedor } from './../../Fornecedor/model/Fornecedor';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { Produto, ProdutoVM } from '../model/Produto';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { PageParams } from 'src/app/shared/models/Params';
import { ResponseData } from 'src/app/shared/models/ResponseData';

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
    codigo: new FormControl('', Validators.required),
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
      codigo: '',
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
  estoqueAtual(produto: Produto): Observable<any> {
    const api = `${this.endpoint}/produto/estoque`;
    return this.http.post(api, produto)
      .pipe(
        map((res: Response) => {
          return res || 0;
        }),
        catchError(this.handleError)
      );
  }

  listar(params: PageParams): Observable<ResponseData<Produto>> {
    const api = `${this.endpoint}/produto/buscar-todos?PageNumber=${params.PageNumber}
                      &PageSize=${params.PageSize}&Filter=${params.Filter}`;
    return this.http.get(api).pipe(
      map((res: ResponseData<Produto>) => {
        return res || new ResponseData<Produto>();
      }),
      catchError(this.handleError)
    );
  }

  listarTodos(): Observable<any> {
    const api = `${this.endpoint}/produto`;
    return this.http.get(api, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || [];
      }),
      catchError(this.handleError)
    );
  }

  buscarProdutos(filter = '', sortOrder = 'asc',
                 pageNumber = 0, pageSize = 5): Observable<any> {
    const api = `${this.endpoint}/produto/buscar-todos`;
    return this.http.get(api, {
      params: new HttpParams()
        .set('filter', filter)
        .set('sortOrder', sortOrder)
        .set('pageNumber', pageNumber.toString())
        .set('pageSize', pageSize.toString())
    }).pipe(
      map(res => res)
    );
  }
}


