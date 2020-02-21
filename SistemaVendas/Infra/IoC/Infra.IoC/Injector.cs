using SistemaVendas.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Infra.Data.Interfaces;
using SistemaVendas.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using SistemaVendas.Core.Domains.Produtos.Interfaces;
using SistemaVendas.Core.Domains.Clientes.Interfaces;
using SistemaVendas.Aplication.InterfaceServices.Auth;
using SistemaVendas.Aplication.Services.Auth;
using SistemaVendas.Aplication.InterfaceServices.Clientes;
using SistemaVendas.Aplication.Services.Clientes;
using SistemaVendas.Aplication.Services.Produtos;
using SistemaVendas.Aplication.InterfaceServices.Produtos;
using SistemaVendas.Aplication.Services.Fornecedores;
using SistemaVendas.Aplication.InterfaceServices.Fornecedores;

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
            services.AddScoped<IClienteRepository, ClienteRepository>();  
            services.AddScoped<IClienteService, ClienteService>();  


        }
    }
}
