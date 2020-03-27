using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class PedidoVenda
    {
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdUsuarioLogado { get; set; }
        public DateTime DataVenda { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual IEnumerable<ItemPedidoVenda> ItemPedidos { get; set; } = new List<ItemPedidoVenda>();
        public double ValorTotal { get; set; }
        public PedidoVenda()
        {

        }
        public PedidoVenda(Guid id, DateTime dataVenda, Cliente cliente, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal , Guid idUsuarioLogado)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
            IdUsuarioLogado = idUsuarioLogado;
        }

        public PedidoVenda( Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal, Guid idUsuarioLogado)
        {
            Id = Guid.NewGuid();
            DataVenda = DateTime.UtcNow;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
            IdUsuarioLogado = idUsuarioLogado;
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

        public PedidoVenda(DateTime dataVenda, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal, Guid idUsuarioLogado)
        {
            Id = Guid.NewGuid();
            DataVenda = dataVenda;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
            IdUsuarioLogado = idUsuarioLogado;
        }

        public PedidoVenda(Guid idCliente, double valorTotal, Guid idUsuarioLogado)
        {
            Id = Guid.NewGuid();
            DataVenda = DateTime.Now;
            IdCliente = idCliente;
            ValorTotal = valorTotal;
            IdUsuarioLogado = idUsuarioLogado;
        }

        public PedidoVenda(Guid id, DateTime dataVenda, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal, Guid idUsuarioLogado)
        {
            Id = id;
            DataVenda = dataVenda;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
            IdUsuarioLogado = idUsuarioLogado;
        }
    }
}
