using SistemaVendas.Infra.Data.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Produtos.Entities;

namespace SistemaVendas.Infra.Data
{
    public class VendasEFContext : DbContext
    {

        public IConfiguration Configuration { get; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoFornecedor> ProdutosFornecidos { get; set; }


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
            
            modelBuilder.Entity<Produto>().ToTable("TB_Produto");
            modelBuilder.ApplyConfiguration(new ProdutoMap());


            modelBuilder.Entity<ProdutoFornecedor>()
                .HasKey(pf => new { pf.IdFornecedor, pf.IdProduto });
            modelBuilder.Entity<ProdutoFornecedor>()
                .HasOne(pf => pf.Produto)
                .WithMany(p => p.ProdutoFornecedores)
                .HasForeignKey(pf => pf.IdProduto);
            modelBuilder.Entity<ProdutoFornecedor>()
                .HasOne(pf => pf.Fornecedor)
                .WithMany(f => f.ProdutosFornecidos)
                .HasForeignKey(pf => pf.IdFornecedor);
        }
    }
}
