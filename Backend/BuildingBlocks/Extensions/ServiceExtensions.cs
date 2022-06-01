using BuildingBlocks.Domain.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Extensions
{
    public static class ServiceExtensions
    {
        //public static IServiceCollection ConfigureMySql<T>(this IServiceCollection services, IConfiguration configuration)
        //    where T : DbContext
        //{
        //    services.AddDbContext<T>(opts =>
        //    {
        //        var str = configuration.GetConnectionString("DefaultConnection");
        //        opts.UseMySql(str, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(str));
        //    });

        //    services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

        //    services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

        //    return services;
        //}

        //public static IServiceCollection ConfigureNpgsql<T>(this IServiceCollection services, IConfiguration configuration)
        //    where T : DbContext
        //{
        //    services.AddDbContext<T>(opts =>
        //    {
        //        var str = configuration.GetConnectionString("DefaultConnection");
        //        opts.UseNpgsql(str);
        //    });

        //    services.AddDatabaseDeveloperPageExceptionFilter();

        //    services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

        //    services.AddScoped<IUnitOfWork, UnitOfWork<T>>();

        //    return services;
        //}


        public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddTransient(serviceProvider =>
            {
                var str = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                var db = configuration.GetValue<string>("ConnectionStrings:Database");

                var mongoClient = new MongoClient(str);
                return mongoClient.GetDatabase(db);
            });

            return services;
        }
   
    }
}
