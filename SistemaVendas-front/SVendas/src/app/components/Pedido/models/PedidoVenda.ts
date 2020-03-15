import { ItemPedidoVenda, ItemPedidoVendaVM } from './ItemPedidoVenda';
import { Cliente, ClienteVM } from '../../Cliente/model/Cliente';

export class PedidoVenda {
    Id: string;
    dataVenda: any;
    cliente: Cliente;
    idCliente: string;
    itemsPedido = new Array<ItemPedidoVenda>();
    valorTotal: number;
}
export class PedidoVendaVM {
    constructor(
        // public  dataVenda = new Date().toJSON,
        public  idCliente: string ,
        public  itemPedidosVM  = new Array<ItemPedidoVendaVM>(),
        public  valorTotal: number
    ) {

    }
}