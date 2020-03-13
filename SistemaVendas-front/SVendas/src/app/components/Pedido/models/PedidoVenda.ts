import { ItemPedidoVenda } from './ItemPedidoVenda';
import { Cliente, ClienteVM } from '../../Cliente/model/Cliente';

export class PedidoVenda {
    Id: string;
    moment = Date.now();
    cliente: Cliente;
    idCliente: string;
    itemsPedido = new Array<ItemPedidoVenda>();
    valorTotal: number;
}
export class PedidoVendaVM {

    constructor(
        public  id: string ,
        public  moment =  Date.now() ,
        public  clienteVM: ClienteVM,
        public  idCliente: string ,
        public  itemPedidosVM  = new Array<ItemPedidoVenda>(),
        public  valorTotal: number
    ) {

    }
}