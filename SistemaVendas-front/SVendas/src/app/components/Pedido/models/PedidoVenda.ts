import { ItemPedidoVenda, ItemPedidoVendaVM } from './ItemPedidoVenda';
import { Cliente, ClienteVM } from '../../Cliente/model/Cliente';

export class PedidoVenda {
    id: string;
    dataVenda: any;
    cliente: Cliente;
    idCliente: string;
    itemPedidos = new Array<ItemPedidoVenda>();
    valorTotal: number;
}
export class PedidoVendaVM {
    constructor(
        public  idCliente: string ,
        public  itemPedidosVM  = new Array<ItemPedidoVendaVM>(),
        public  valorTotal: number,
        public  dataVenda?,
        public  id?,

    ) {

    }
}