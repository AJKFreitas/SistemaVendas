import { Injectable } from '@angular/core';

import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { ToastService } from 'src/app/components/Shared/ToastService';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private authService: AuthService,
        private toastSevice: ToastService, ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                this.toastSevice.Warning('Usuário não autorizado!', 'Atenção!');
            }
            if (err.status === 500) {
                this.toastSevice.Error('Erro no servidor, tente novamente mais tarde!');
            }
            if (err.status === 403) {
                this.toastSevice.Warning('Seu perfil de usuário nao pode executar essa ação!', 'Forbidden!');
            }
            if (err.status === 415) {
               this.toastSevice.Warning('O formato de mídia dos dados requisitados não é suportado pelo servidor.',
                'Unsupported Media Type!');
            }

            const error = err.error.message || err.statusText;
            return throwError(error);
        }));
    }
}