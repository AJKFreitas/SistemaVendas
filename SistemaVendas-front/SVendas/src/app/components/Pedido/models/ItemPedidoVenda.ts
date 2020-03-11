import { Produto } from '../../Produto/model/Produto';

export class ItemPedidoVenda {
    constructor(
        public id: string,
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public produto: Produto,
        public idPedido: string,
        public estoque?: number
        ){}
}