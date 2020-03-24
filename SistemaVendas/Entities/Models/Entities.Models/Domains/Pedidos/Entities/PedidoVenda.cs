using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class PedidoVenda
    {
        public Guid Id { get; set; }
        public DateTime DataVenda { get; set; }
        public virtual Cliente Cliente { get; set; }
        public Guid IdCliente { get; set; }
        public virtual IEnumerable<ItemPedidoVenda> ItemPedidos { get; set; } = new List<ItemPedidoVenda>();
        public double ValorTotal { get; set; }

        public PedidoVenda()
        {

        }
        public PedidoVenda(Guid id, DateTime dataVenda, Cliente cliente, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            Cliente = cliente;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }

        public PedidoVenda( Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = Guid.NewGuid();
            DataVenda = DateTime.UtcNow;
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

        public PedidoVenda(DateTime dataVenda, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = Guid.NewGuid();
            DataVenda = dataVenda;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }

        public PedidoVenda(Guid idCliente, double valorTotal)
        {
            Id = Guid.NewGuid();
            DataVenda = DateTime.Now;
            IdCliente = idCliente;
            ValorTotal = valorTotal;
        }

        public PedidoVenda(Guid id, DateTime dataVenda, Guid idCliente, IEnumerable<ItemPedidoVenda> itemPedidos, double valorTotal)
        {
            Id = id;
            DataVenda = dataVenda;
            IdCliente = idCliente;
            ItemPedidos = itemPedidos;
            ValorTotal = valorTotal;
        }
    }
}
//SELECT(
//SELECT ifnull(SUM(quantidade),0) from tb_itemordemcompra where idproduto = '0f6c5175-beb5-44bd-9236-17d314286415')
//-
//(SELECT ifnull(SUM(quantidade),0) from TB_ItemPedido where idproduto = '0f6c5175-beb5-44bd-9236-17d314286415') estoque