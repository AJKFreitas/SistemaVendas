export class ItemPedidoVenda {
    estoque = 0;
    constructor(
        public id: string,
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public idProduto: string,
        public idPedido: string,
        ){}
}