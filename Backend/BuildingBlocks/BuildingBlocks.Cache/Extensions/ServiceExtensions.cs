using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Cache.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var server = configuration.GetValue<string>("Cache:Server");
            var port = configuration.GetValue<string>("Cache:Port");

            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = $"{server}:{port}";
            });

            return services;
        }
    }
}
