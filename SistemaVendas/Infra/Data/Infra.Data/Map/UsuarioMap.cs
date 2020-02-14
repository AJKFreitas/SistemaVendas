using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Infra.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_Usuario");
            builder.Property(Usuario => Usuario.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(Usuario => Usuario.Nome).HasColumnName("Nome");
            builder.Property(Usuario => Usuario.Email).HasColumnName("Email");
            builder.Property(Usuario => Usuario.Senha).HasColumnName("Senha");
            builder.Property(Usuario => Usuario.Role).HasColumnName("Role");
            builder.Property(Usuario => Usuario.IsAdmin).HasColumnName("IsAdmin");
        }
    }
}
