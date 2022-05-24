using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Mongo;
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
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
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

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collection)
            where T : IAggregateRoot
        {

            services.AddSingleton<IMongoRepository<T>>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                if (configuration == null)
                { throw new Exception(nameof(configuration)); }

                var str = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                var database = configuration.GetValue<string>("ConnectionStrings:Database");

                return new MongoRepository<T>(str, database, collection);
            });


            return services;
        }
    }
}
