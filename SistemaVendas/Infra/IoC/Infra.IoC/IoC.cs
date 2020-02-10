using Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Core.Domains.Usuario.Services;
using SistemaVendas.Core.Domains.Usuario.Services.Interfaces;

namespace Infra.IoC
{
    public class IoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<VendasEFContext>();
            services.AddScoped<UsuarioService, IUsuarioService>();
        }
    }
}
