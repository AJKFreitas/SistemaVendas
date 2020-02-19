using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Produtos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("TB_Produto");
            builder.Property(Fornecedor => Fornecedor.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.HasMany(produto => produto.ItemPedidos)
                   .WithOne(intemPedido => intemPedido.Produto)
                   .HasForeignKey(itemPedido => itemPedido.IdProduto);
        }
    }
}
