using MediaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MediaContext>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
            });

            return services;
        }


        public static async Task UseInitialMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            using var database = scope.ServiceProvider.GetService<MediaContext>();
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
