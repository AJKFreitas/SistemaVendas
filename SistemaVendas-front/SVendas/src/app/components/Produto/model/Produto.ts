import { Fornecedor } from '../../Fornecedor/model/Fornecedor';

export class Produto {
    id: string;
    nome: string;
    descricao: string;
    valor: number;
    codigo: number;
    fornecedores: Fornecedor[];
}
export class ProdutoVM {

    constructor(
        public nome: string,
        public descricao: string,
        public valor: number,
        public codigo: number,
        public fornecedores: Fornecedor[]
    ) { }
}
export class ProdutoItemPedido {
    id: string;
    nome: string;
    descricao: string;
    valor: number;
    codigo: number;
    estoqueAtual: number;
    public fornecedores: Fornecedor[]
}