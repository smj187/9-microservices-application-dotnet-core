using CatalogService.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton<ICatalogContext>(opts =>
            {
                var str = configuration.GetConnectionString("MongoDB");
                var database = configuration.GetConnectionString("Database");
                return new CatalogContext(str, database);
            });
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
