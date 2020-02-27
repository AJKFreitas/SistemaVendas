import { ItemPedidoVenda } from './ItemPedidoVenda'
import { Cliente } from '../../Cliente/model/Cliente';

export class PedidoVenda {
    Id: string;
    moment = Date.now();
    cliente: Cliente;
    idCliente: string;
    itemsPedido = new Array<ItemPedidoVenda>();
    valorTotal: number;
}