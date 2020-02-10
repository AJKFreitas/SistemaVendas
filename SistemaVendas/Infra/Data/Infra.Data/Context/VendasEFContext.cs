using Entities.Models.Usuario;
using Infra.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data
{
   public class VendasEFContext : DbContext
    {

        public IConfiguration Configuration { get; }
        public DbSet<Usuario> Usuarios { get; set; }

        public VendasEFContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //Configuration["EndPointApi:ADAuthentication"]
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("TB_Usuario");
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
