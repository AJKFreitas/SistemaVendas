using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
  public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime Moment { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Guid IdCliente { get; set; }
        public virtual IEnumerable<ItemPedido> ItemPedidos { get; set; } = new List<ItemPedido>();
        public double ValorTotal { get; set; }

        public Pedido()
        {

        }
        public Pedido(Guid id, DateTime moment, Cliente cliente, Guid idCliente, IEnumerable<ItemPedido> itemPedidos, double valorTotal)
        {
            Id = id;
            Moment = moment;
            Cliente = cliente;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }

        public Pedido(DateTime moment, Cliente cliente, Guid idCliente, IEnumerable<ItemPedido> itemPedidos, double valorTotal)
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
            foreach (ItemPedido item in ItemPedidos)
            {
                sum += item.CalcSubTotal();
            }
            return sum;
        }

       

    }
}
