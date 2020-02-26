export class ItemPedidoVenda {
    constructor(
        public id: string,
        public quantidade: number,
        public preco: number,
        public subTotal: number,
        public idProduto: string,
        public idPedido: string,
    ){}
}