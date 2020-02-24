using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Pedidos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
  public  class OrdemCompraMap : IEntityTypeConfiguration<OrdemCompra>
    {
        public void Configure(EntityTypeBuilder<OrdemCompra> builder)
        {
            builder.HasMany(ordemCompra => ordemCompra.ItemsOrdemCompra)
                   .WithOne(itemsOrdemCompra => itemsOrdemCompra.OrdemCompra)
                   .HasForeignKey(itemsOrdemCompra => itemsOrdemCompra.IdOrdemCompra);

            builder.HasOne(ordemCompra => ordemCompra.Fornecedor)
                .WithMany(fornecedor => fornecedor.OrdemCompras)
                .HasForeignKey(ordemCompra => ordemCompra.IdFornecedor);
        }

       
    }
}
