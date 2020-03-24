import { Produto } from '../../Produto/model/Produto';
import { OrdemCompra } from './OrdemCompra';

export class ItemOrdemCompra {

    constructor(
        public id: string,
        public idProduto: string,
        public idOrdemCompra: string,
        public preco: number,
        public subTotal: number,
        public quantidade: number,
        public ordemCompra: OrdemCompra,
        public produto: Produto,
    ) { }
    estoque;

}
export class ItemOrdemCompraVM {
    constructor(
        public idProduto: string,
        public idOrdemCompra: string,
        public preco: number,
        public quantidade: number,
        public subTotal: number,
        public id?: string,
    ) { }
}
