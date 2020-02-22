﻿using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Pedidos.Entities
{
    public class ItemPedidoVenda
    {
        public Guid Id { get; set; }
        public long Quantidade { get; set; }
        public double Preco { get; set; }
        public double SubTotal { get; set; }
        public virtual Produto Produto { get; set; }
        public Guid IdProduto { get; set; }
        public virtual PedidoVenda Pedido { get; set; }
        public Guid IdPedido { get; set; }

        public ItemPedidoVenda()
        {

        }
        public ItemPedidoVenda(Guid id, long quantidade, double preco, double subTotal, Produto produto, Guid idProduto, PedidoVenda pedido, Guid idPedido)
        {
            Id = id;
            Quantidade = quantidade;
            Preco = preco;
            SubTotal = subTotal;
            Produto = produto;
            IdProduto = idProduto;
            Pedido = pedido;
            IdPedido = idPedido;
        }

        public ItemPedidoVenda(long quantidade, double preco, double subTotal, Produto produto, Guid idProduto, PedidoVenda pedido, Guid idPedido)
        {
            Id = Guid.NewGuid();
            Quantidade = quantidade;
            Preco = preco;
            SubTotal = subTotal;
            Produto = produto;
            IdProduto = idProduto;
            Pedido = pedido;
            IdPedido = idPedido;
        }

        public double CalcSubTotal()
        {
            return Quantidade * Preco;
        }

    }
}