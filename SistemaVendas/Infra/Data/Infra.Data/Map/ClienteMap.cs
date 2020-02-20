using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Clientes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
   public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("TB_Cliente");
            builder.Property(cliente => cliente.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(cliente => cliente.Nome).HasColumnName("Nome");
            builder.Property(cliente => cliente.Endereco).HasColumnName("Endereco");
            builder.Property(cliente => cliente.CPF).HasColumnName("CPF");
            builder.Property(cliente => cliente.Telefone).HasColumnName("Telefone");

            builder.HasMany(cliente => cliente.Pedidos)
                    .WithOne(pedido => pedido.Cliente)
                    .HasForeignKey(pedido => pedido.IdCliente);
        }
    }
}
