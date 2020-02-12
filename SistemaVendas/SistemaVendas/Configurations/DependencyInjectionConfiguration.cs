using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace SistemaVendas.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            SistemaVendas.Infra.IoC.Injector.RegisterServices(services, configuration);
        }
    }
}
