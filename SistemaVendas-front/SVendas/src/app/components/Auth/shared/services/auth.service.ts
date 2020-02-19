import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { Usuario } from '../models/User';
import { catchError, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/LoginUser';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  currentUser = {};
  profileContext: string;

  usuarioAutenticado = false;
  mostrarMenuEmitter: EventEmitter<boolean> = new EventEmitter();
  constructor(
    private http: HttpClient,
    public router: Router,
    public jwtHelper: JwtHelperService,
    private SpinnerService: NgxSpinnerService,
    private route: ActivatedRoute
  ) {
  }

  // Sign-up
  signUp(user: Usuario): Observable<any> {
    const api = `${this.endpoint}/usuario`;
    return this.http.post(api, user)
      .pipe(
        catchError(this.handleError)
      )
  }

  // Sign-in
  signIn(user: LoginUser) {
    this.SpinnerService.show();
    return this.http.post<any>(`${this.endpoint}/login`, user)
      .subscribe((res: any) => {
        console.log(res);
        localStorage.setItem('access_token', res.accessToken);
        this.SpinnerService.hide();
        this.router.navigate(['dashboard'], { relativeTo: this.route }).then(nav => {
          window.location.reload();
        });
      },
        err => {
          this.SpinnerService.hide();
        });
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  get isLoggedIn(): boolean {
    const authToken = localStorage.getItem('access_token');
    return (authToken !== null) ? true : false;
  }

  doLogout() {
    const removeToken = localStorage.removeItem('access_token');
    if (removeToken == null) {
      this.router.navigate(['log-in']);
    }
    this.SpinnerService.hide();
  }

  get getProfileContext(): string {
    return this.profileContext;
  }
  setProfileContext(text: string) {
    this.profileContext = text;
  }
  // User profile
  getUserProfile(id): Observable<any> {
    const api = `${this.endpoint}/usuario/${id}`;
    return this.http.get(api, { headers: this.headers }).pipe(
      map((res: Response) => {
        return res || {}
      }),
      catchError(this.handleError)
    )
  }

  // Error 
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

