import { Parametros } from './../../../../shared/models/Params';

export class Usuario {
    id: string;
    nome: string;
    email: string;
    senha: string;
    role: string;
}
export class UsuarioVM {

    constructor(
        public nome: string,
        public email: string,
        public senha: string,
        public role: string) { }

}
export class UsuarioParams extends Parametros {
    constructor(
        public TamanhoDaPagina: number,
        public NumeroDaPaginaAtual: number

    ) {
        super(TamanhoDaPagina, NumeroDaPaginaAtual);
    }
}
