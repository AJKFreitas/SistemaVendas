using Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.IoC
{
    public class IoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<VendasEFContext>();
        }
    }
}
