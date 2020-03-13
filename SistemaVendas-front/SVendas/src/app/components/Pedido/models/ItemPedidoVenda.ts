import { Produto } from '../../Produto/model/Produto';

export class ItemPedidoVenda {
     precoCompra;
    constructor(
        public id: string,
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public produto: Produto = new Produto(),
        public idPedido: string,
        public estoque?: number,
        public idProduto?: string,
        ){}
}