using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("TB_Fornecedor");
            builder.Property(Fornecedor => Fornecedor.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(Fornecedor => Fornecedor.Nome).HasColumnName("Nome"); 
        }
    }
}
