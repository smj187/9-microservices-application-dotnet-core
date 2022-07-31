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
            var connection = configuration.GetValue<string>("Cache:DefaultConnection");
            var port = configuration.GetValue<int>("Cache:DefaultPort");
            var database = configuration.GetValue<string>("Cache:Database");

            services.AddEasyCaching(opts =>
            {
                opts.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint(connection, port));
                    config.DBConfig.SyncTimeout = 10000;
                    config.DBConfig.AsyncTimeout = 10000;
                    config.SerializerName = "jsonserializer";
                }, database)
                .WithMessagePack("jsonserializer");
            });
            return services;
        }
    }
}
