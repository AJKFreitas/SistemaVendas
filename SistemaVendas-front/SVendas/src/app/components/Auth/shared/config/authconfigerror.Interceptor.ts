import { Injectable } from '@angular/core';

import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';

import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { MensagemPopUPService } from 'src/app/components/Shared/ToastService';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private toastSevice: MensagemPopUPService,
        private spinnerService: NgxSpinnerService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                this.toastSevice.Aviso('Usuário não autorizado!', 'Atenção!');
            }
            if (err.status === 500) {
                this.toastSevice.Erro('Erro no servidor, tente novamente mais tarde!');
            }
            if (err.status === 403) {
                this.toastSevice.Aviso('Seu perfil de usuário nao pode executar essa ação!', 'Forbidden!');
                window.history.back();
                this.spinnerService.hide();
            }
            if (err.status === 415) {
                this.toastSevice.Aviso('Unsupported Media Type!',
                    'O formato de mídia dos dados requisitados não é suportado pelo servidor.');
            }
            if (err.status === 423) {
                this.toastSevice.Aviso('Recurso já existe', 'Já existe um recurso com esses dados cadastrado');
            }
            if (err.status === 404) {
                this.toastSevice.Erro('Recurso não encontrado ou não existe!');
            }
            // tslint:disable-next-line:no-debugger
            if (err.error) {
                this.toastSevice.Erro(err.error);
            }
            const error = err.error.message || err.statusText;
            return throwError(error);
        }));
    }
}
