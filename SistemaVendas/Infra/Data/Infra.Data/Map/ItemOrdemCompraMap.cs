using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
    class ItemOrdemCompraMap : IEntityTypeConfiguration<ItemOrdemCompra>
    {
        public void Configure(EntityTypeBuilder<ItemOrdemCompra> builder)
        {
            builder.HasOne(itemOrdemCompra => itemOrdemCompra.Produto)
                    .WithMany(produto => produto.ItemOrdemCompras)
                    .HasForeignKey(itemPedido => itemPedido.IdProduto);

            builder.HasOne(itemOrdemCompra => itemOrdemCompra.OrdemCompra)
                    .WithMany(ordemCompra => ordemCompra.ItemsOrdemCompra)
                    .HasForeignKey(itemOrdemCompra => itemOrdemCompra.IdOrdemCompra);
        }
    
    }
}
