using SistemaVendas.Infra.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Entities;

namespace SistemaVendas.Infra.Data
{
    public class VendasEFContext : DbContext
    {

        public IConfiguration Configuration { get; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public VendasEFContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public VendasEFContext()
        {
        }

  //ççç
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>().ToTable("TB_Usuario");
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            modelBuilder.Entity<Fornecedor>().ToTable("TB_Fornecedor");
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.Entity<Fornecedor>().Property(fornecedor => fornecedor.TipoUsuario).HasConversion<int>();
        }
    }
}
