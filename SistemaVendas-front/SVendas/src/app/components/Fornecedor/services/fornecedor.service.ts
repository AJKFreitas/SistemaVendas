import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router, Params } from '@angular/router';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
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

  // Sign-up
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


  editar(usuario: Fornecedor): Observable<any> {
    const api = `${this.endpoint}/fornecedor`;
    return this.http.put(api, usuario)
      .pipe(
        catchError(this.handleError)
      );
  }
  deletar(fornecedor: Fornecedor) {
    const api = `${this.endpoint}/usuario/${fornecedor.id}`;
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


}
