using SistemaVendas.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Core.Domains.Auth.Services;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using SistemaVendas.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
        }
    }
}
