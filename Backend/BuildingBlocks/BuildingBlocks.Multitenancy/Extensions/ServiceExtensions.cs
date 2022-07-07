using BuildingBlocks.Multitenancy.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMySqlMultitenancy<T>(this IServiceCollection services, IConfiguration config)
            where T : DbContext
        {
            services.AddDbContext<T>(opts =>
            {
                var str = config.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
            });

            var tenants = config.GetSection("tenants").Get<IEnumerable<TenantConfiguration>>();
            foreach (var tenant in tenants)
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<T>();

                var str = tenant.ConnectionString;
                dbContext.Database.SetConnectionString(tenant.ConnectionString);

                if (dbContext.Database.GetMigrations().Any())
                {
                    dbContext.Database.Migrate();
                }
            }

            

            return services;
        }
    }
}
