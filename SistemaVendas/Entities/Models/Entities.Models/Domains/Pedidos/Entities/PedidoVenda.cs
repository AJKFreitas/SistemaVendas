using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class PedidoVenda
    {
        public Guid Id { get; set; }
        public DateTime Moment { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Guid IdCliente { get; set; }
        public virtual IEnumerable<ItemPedidoVenda> ItemPedidos { get; set; } = new List<ItemPedidoVenda>();
        public double ValorTotal { get; set; }

        public PedidoVenda()
        {

        }
        public PedidoVenda(Guid id, DateTime moment, Cliente cliente, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = id;
            Moment = moment;
            Cliente = cliente;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }

        public PedidoVenda(DateTime moment, Cliente cliente, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = Guid.NewGuid();
            Moment = moment;
            Cliente = cliente;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }

        public double Total()
        {
            double sum = 0;
            foreach (ItemPedidoVenda item in ItemPedidos)
            {
                sum += item.CalcSubTotal();
            }
            return sum;
        }

        public PedidoVenda(DateTime moment, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = Guid.NewGuid();
            Moment = moment;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }
    }
}
