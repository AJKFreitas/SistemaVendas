import { Fornecedor } from '../../Fornecedor/model/Fornecedor';
import { ItemOrdemCompra, ItemOrdemCompraVM } from './ItemOrdemCompra';

export class OrdemCompra {

    id: string;
    dataEntrada: Date;
    nota: string;
    fornecedor: Fornecedor;
    idFornecedor: string;
    itemsOrdemCompra: Array<ItemOrdemCompra> = new Array<ItemOrdemCompra>();
    valorTotal: number;
}

export class OrdemCompraVM {
    constructor(
        public idFornecedor: string,
        public itemsOrdemCompraVM: Array<ItemOrdemCompraVM> = new Array<ItemOrdemCompraVM>(),
        public valorTotal: number,
        public nota?: string,
        public dataEntrada?: Date,
        public id?: string,
    ) { }
}
