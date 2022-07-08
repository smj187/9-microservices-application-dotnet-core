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

namespace BuildingBlocks.Mongo.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient(serviceProvider =>
            //{
            //    var str = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            //    var db = configuration.GetValue<string>("ConnectionStrings:Database");

            //    var mongoClient = new MongoClient(str);
            //    return mongoClient.GetDatabase(db);
            //});

            return services;
        }
    }
}
