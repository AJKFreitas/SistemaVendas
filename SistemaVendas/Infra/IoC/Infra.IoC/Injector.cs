using SistemaVendas.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Core.Domains.Auth.Services;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Infra.Data.Interfaces;
using SistemaVendas.Infra.Data.Repository;

namespace SistemaVendas.Infra.IoC
{
    public class Injector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<VendasEFContext>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
