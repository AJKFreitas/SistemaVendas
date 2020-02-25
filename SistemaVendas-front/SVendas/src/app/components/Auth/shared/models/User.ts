import { Params } from 'src/app/shared/models/Params';

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
export class UsuarioParams extends Params {
    constructor(
        public pageSize: number,
        public pageNumber: number

    ) {
        super(pageSize, pageNumber);
    }
}