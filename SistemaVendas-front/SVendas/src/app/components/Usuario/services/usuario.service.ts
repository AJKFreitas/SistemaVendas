import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Router, Params } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { Usuario, UsuarioVM } from '../../Auth/shared/models/User';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  roles: string[] = ['Admin', 'Funcionario', 'Fornecedor', 'Vendedor'];
  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  constructor(
    private http: HttpClient,
    public router: Router
  ) { }

  public form: FormGroup = new FormGroup({
    id: new FormControl(''),
    nome: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.email, Validators.required]),
    senha: new FormControl('', Validators.required),
    role: new FormControl('', Validators.required),
  });


  iserir(usuario: UsuarioVM): Observable<any> {
    const api = `${this.endpoint}/usuario`;
    return this.http.post(api, usuario)
      .pipe(
        catchError(this.handleError)
      );

  }
  editar(usuario: Usuario): Observable<any> {
    const api = `${this.endpoint}/usuario`;
    return this.http.put(api, usuario)
      .pipe(
        catchError(this.handleError)
      );
  }
  deletar(usuario: Usuario) {
    const api = `${this.endpoint}/usuario/${usuario.id}`;
    return this.http.delete(api)
      .pipe(
        catchError(this.handleError)
      );
  }

  listar(params: Params): Observable<any> {
    const api = `${this.endpoint}/usuario/buscar-todos`;
    return this.http.post(api, params).pipe(
      map((res: Response) => {
        return res || []
      }),
      catchError(this.handleError)
    );
  }

  public resetForm() {
    this.form.reset();
    this.form.setErrors = null;
  }
  initializeFormGroup() {
    this.form.setValue({
      id: '',
      email: '',
      nome: '',
      senha: '',
      role: '',
    });
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
  buscarUsuarios(filter = '', sortOrder = 'asc',
                 pageNumber = 0, pageSize = 5): Observable<any> {
    const api = `${this.endpoint}/usuario/buscar-todos`;
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
