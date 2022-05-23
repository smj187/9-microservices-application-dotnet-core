using Microsoft.EntityFrameworkCore;
using PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentContext>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseNpgsql(str);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static async Task UseInitialMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            using var database = scope.ServiceProvider.GetService<PaymentContext>();
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
