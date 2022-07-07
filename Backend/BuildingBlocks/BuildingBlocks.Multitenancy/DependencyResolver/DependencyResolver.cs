using BuildingBlocks.Multitenancy.Interfaces.Services;
using BuildingBlocks.Multitenancy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.DependencyResolver
{
    public class DependencyResolver
    {
        public IServiceProvider ServiceProvider { get; set; }

        public DependencyResolver()
        {
            var services = new ServiceCollection();

            services.AddTransient<IEnvironmentService, EnvironmentService>();
            services.AddTransient<IConfigurationService, ConfigurationService>(serviceProvider =>
            {
                return new ConfigurationService(serviceProvider.GetRequiredService<IEnvironmentService>(), Directory.GetCurrentDirectory());
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IMultitenancyService>(serviceProvider =>
            {
                var configService = serviceProvider.GetRequiredService<IConfigurationService>().GetConfiguration();
                var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                return new MultitenancyService(httpContext, configService);
            });

            ServiceProvider = services.BuildServiceProvider();
        }

        public IMultitenancyService GetTenantService() 
            => ServiceProvider.GetRequiredService<IMultitenancyService>();

        public IConfiguration GetConfiguration() 
            => ServiceProvider.GetRequiredService<IConfigurationService>().GetConfiguration();
    }
}
