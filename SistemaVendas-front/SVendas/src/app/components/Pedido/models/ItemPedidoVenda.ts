import { Produto } from '../../Produto/model/Produto';

export class ItemPedidoVenda {
    precoCompra?: number;
    constructor(
        public id: string,
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public produto: Produto = new Produto(),
        public idPedido: string,
        public estoque?: number,
        public idProduto?: string,
    ) { }
}
export class ItemPedidoVendaVM {
    constructor(
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public idProduto: string,
        public idPedido: string,
    ) { }
}