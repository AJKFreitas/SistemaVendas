import { PedidoVenda } from '../../Pedido/models/PedidoVenda';

export class Cliente {
    id: string;
    nome: string;
    cpf: number;
    telefone: string;
    endereco: string;
    Pedidos: PedidoVenda [] = [];
}
export class ClienteVM {

    constructor(
        public nome: string,
        public cpf: number,
        public telefone: string,
        public endereco: string) { }

}