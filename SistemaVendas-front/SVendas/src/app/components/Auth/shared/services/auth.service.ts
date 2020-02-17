import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { Usuario } from '../models/User';
import { catchError, map, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/LoginUser';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: 'root'
})

export class AuthService {

  endpoint: string = environment.apiVendas;
  headers = new HttpHeaders().set('Content-Type', 'application/json');
  currentUser = {};
  profileContext: string ;

  usuarioAutenticado = false;
  mostrarMenuEmitter: EventEmitter<boolean> = new EventEmitter();
  constructor(
    private http: HttpClient,
    public router: Router,
    public jwtHelper: JwtHelperService
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
    return this.http.post<any>(`${this.endpoint}/login`, user)
      .subscribe((res: any) => {
        localStorage.setItem('access_token', res.accessToken);
        debugger;
        const token = this.jwtHelper.decodeToken(this.getToken());
<<<<<<< HEAD
        /*this.getUserProfile(token.data?.id).subscribe((res) => {
=======
        this.getUserProfile(token.data.id).subscribe((res) => {
>>>>>>> 084835aa857991868a738f40c363748a74baed02
          if (token?.data?.id) {
            this.currentUser = res;
            this.usuarioAutenticado = true;
            this.mostrarMenuEmitter.emit(true);
            this.router.navigate(['user-profile/' + token.data.id]);
          } else {
              this.usuarioAutenticado = false;
              this.mostrarMenuEmitter.emit(false);
          }
        })
<<<<<<< HEAD
        */
=======
>>>>>>> 084835aa857991868a738f40c363748a74baed02
        console.log(token);
        this.router.navigate(['dashboard']);
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
  }

  get getProfileContext(): string {
    return this.profileContext ;
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

