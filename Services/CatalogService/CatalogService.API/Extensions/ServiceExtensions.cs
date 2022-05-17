using CatalogService.Application.Repositories.Categories;
using CatalogService.Application.Repositories.Products;
using CatalogService.Core.Entities.Base;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddScoped(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                var str = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                var client = new MongoClient();
                var db = configuration.GetValue<string>("ConnectionStrings:Database");
                var database = client.GetDatabase(db);
                //return database;
                return new CatalogContext(configuration);
            });

            services.AddTransient(serviceProvider =>
            {
                var str = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                var client = new MongoClient();
                var db = configuration.GetValue<string>("ConnectionStrings:Database");
                var database = client.GetDatabase(db);
                var mongoClient = new MongoClient(str);
                return mongoClient.GetDatabase("Catalog");
            });

            return services;
        }

        public static IServiceCollection AddProductRepository(this IServiceCollection services, string mongoCollectionName)
        {
            // product
            services.AddTransient<IProductQueryRepository>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                if (configuration == null)
                { throw new Exception(nameof(configuration)); }

                var db = sp.GetRequiredService<IMongoDatabase>();
                return new ProductQueryRepository(configuration, mongoCollectionName);
            });
            
            services.AddTransient<IProductCommandRepository>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                if (configuration == null)
                { throw new Exception(nameof(configuration)); }

                var db = sp.GetRequiredService<IMongoDatabase>();
                return new ProductCommandRepository(configuration, mongoCollectionName);
            });

            return services;
        }
        public static IServiceCollection AddCategoryRepository(this IServiceCollection services, string mongoCollectionName)
        {
            // category
            services.AddTransient<ICategoryQueryRepository>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                if (configuration == null)
                { throw new Exception(nameof(configuration)); }

                var db = sp.GetRequiredService<IMongoDatabase>();
                return new CategoryQueryRepository(configuration, mongoCollectionName);
            });
            
            services.AddTransient<ICategoryCommandRepository>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                if (configuration == null)
                { throw new Exception(nameof(configuration)); }

                var db = sp.GetRequiredService<IMongoDatabase>();
                return new CategoryCommandRepository(configuration, mongoCollectionName);
            });

            return services;
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
