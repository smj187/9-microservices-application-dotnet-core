using BuildingBlocks.EfCore.Repositories;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMySqlDatabase<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, ServerVersion.AutoDetect(str));
            });

            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

            return services;
        }

        public static IServiceCollection ConfigureMySql<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseMySql(str, ServerVersion.AutoDetect(str));
            });

            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

            return services;
        }


        public static IServiceCollection AddPostgresDatabase<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(opts =>
            {
                var str = configuration.GetConnectionString("DefaultConnection");
                opts.UseNpgsql(str);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

            return services;
        }
    }
}
