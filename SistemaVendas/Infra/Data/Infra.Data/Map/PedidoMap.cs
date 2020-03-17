﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Pedidos.Entities;

namespace SistemaVendas.Infra.Data.Map
{
    public class PedidoMap : IEntityTypeConfiguration<PedidoVenda>
    {
        public void Configure(EntityTypeBuilder<PedidoVenda> builder)
        {
            builder.HasMany(pedido => pedido.ItemPedidos)
                   .WithOne(intemPedido => intemPedido.Pedido)
                   .HasForeignKey(itemPedido => itemPedido.IdPedido)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pedido => pedido.Cliente)
                .WithMany(cliente => cliente.Pedidos)
                .HasForeignKey(pedido  => pedido.IdCliente);
        }
    }
}
