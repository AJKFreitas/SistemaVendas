using Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using SistemaVendas.Core.Domains.Auth.Entities;
using SistemaVendas.Core.Domains.Auth.Services;
using SistemaVendas.Core.Domains.Auth.Services.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using SistemaVendas.Infra.Data.Interfaces;
using SistemaVendas.Infra.Data.Repository;

namespace Infra.IoC
{
    public class IoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<VendasEFContext>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
