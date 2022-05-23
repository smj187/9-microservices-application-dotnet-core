using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Infrastructure.Data;

namespace TenantService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureTenant(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TenantContext>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
            });
            return services;
        }

        public static async Task UseInitialMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            using var database = scope.ServiceProvider.GetService<TenantContext>();
            await database.Database.MigrateAsync();
        }

        public static void UseDevEnvironment(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
