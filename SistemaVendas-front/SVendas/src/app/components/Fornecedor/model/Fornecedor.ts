import { Produto } from '../../Produto/model/Produto';

export class Fornecedor {

    id: string;
    nome: string;
    telefone: string;
    cnpj: number;
    produtos: Produto[];
}
export class FornecedorVM {

    constructor(
        public nome: string,
        public cnpj: number,
        public telefone: string,
    ) { }

}
