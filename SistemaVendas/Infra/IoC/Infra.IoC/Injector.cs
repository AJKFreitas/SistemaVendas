using SistemaVendas.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Core.Domains.Auth.Services;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using SistemaVendas.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Shared.Interfaces;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using SistemaVendas.Core.Domains.Fornecedores.Services.interfaces;
using SistemaVendas.Core.Domains.Fornecedores.Services;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Services.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Services;

namespace SistemaVendas.Infra.IoC
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
          
            services.AddScoped<VendasEFContext>();
            services.AddDbContext<VendasEFContext>(options => options.UseMySql(configuration.GetConnectionString("MysqlConnectionString")));
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();  
            services.AddScoped<IAuthenticationService, JwtIdentityAuthenticationService>(); 
            services.AddScoped<IUsuario, Usuario>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFornecedorService, FornecedorService>(); 
            services.AddScoped<IProdutoRepository, ProdutoRepository>();  
            services.AddScoped<IProdutoService, ProdutoService>();  


        }
    }
}
