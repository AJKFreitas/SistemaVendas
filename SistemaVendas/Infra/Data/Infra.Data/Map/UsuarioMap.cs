using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaVendas.Core.Domains.Auth.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_Usuario");
            builder.Property(Usuario => Usuario.Id).ValueGeneratedOnAdd();
            builder.Property(Usuario => Usuario.Nome);
            builder.Property(Usuario => Usuario.Login);
            builder.Property(Usuario => Usuario.Senha);
        }
    }
}
