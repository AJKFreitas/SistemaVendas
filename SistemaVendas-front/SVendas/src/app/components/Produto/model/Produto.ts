import { Fornecedor } from '../../Fornecedor/model/Fornecedor';

export class Produto {
    id: string;
    nome: string;
    descricao: string;
    valor: number;
    produtoFornecedores: Fornecedor[];
}