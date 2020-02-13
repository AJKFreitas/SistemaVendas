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
            builder.Property(Usuario => Usuario.Id).ValueGeneratedOnAdd();
            builder.Property(Usuario => Usuario.Nome);
            builder.Property(Usuario => Usuario.Email);
            builder.Property(Usuario => Usuario.Senha);
            builder.Property(Usuario => Usuario.Role);
            builder.Property(Usuario => Usuario.IsAdmin);
        }
    }
}
