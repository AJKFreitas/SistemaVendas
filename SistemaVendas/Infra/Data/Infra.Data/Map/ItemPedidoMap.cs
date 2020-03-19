using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
    class ItemPedidoMap : IEntityTypeConfiguration<ItemPedidoVenda>
    {
        public void Configure(EntityTypeBuilder<ItemPedidoVenda> builder)
        {
            builder.HasOne(itemPedido => itemPedido.Produto)
                    .WithMany(produto => produto.ItemPedidos)
                    .HasForeignKey(itemPedido => itemPedido.IdProduto);

            builder.HasOne(itemPedido => itemPedido.Pedido)
                    .WithMany(pedido => pedido.ItemPedidos)
                    .HasForeignKey(itemPedido => itemPedido.IdPedido)
                    .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
