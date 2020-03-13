import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from './Message';

declare var toastr: any;

@Injectable()
export class MensagemPopUPService {
    constructor() { }

    Sucesso(titulo: string, mensagem?: string) {
        toastr.success(mensagem, titulo);
    }
    Aviso(titulo: string, mensagem?: string) {
        toastr.warning(mensagem, titulo);
    }

    Erro(titulo: string, mensagem?: string) {
        toastr.error(mensagem, titulo);
    }

    Informacao(mensagem: string) {
        toastr.info(mensagem);
    }
}