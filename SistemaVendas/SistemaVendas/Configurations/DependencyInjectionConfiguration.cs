using Microsoft.Extensions.DependencyInjection;


namespace SistemaVendas.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            SistemaVendas.Infra.IoC.Injector.RegisterServices(services);
        }
    }
}
