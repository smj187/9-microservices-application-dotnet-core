﻿using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<OrderContext>(opts =>
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
            using var database = scope.ServiceProvider.GetService<OrderContext>();
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