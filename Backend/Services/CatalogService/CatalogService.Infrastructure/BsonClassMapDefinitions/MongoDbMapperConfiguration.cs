using CatalogService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.BsonClassMapDefinitions
{
    public static class MongoDbMapperConfiguration
    {
        public static IServiceCollection AddBsonClassMappings(this IServiceCollection services)
        {
            SetBsonClassMapping.Apply();
            CategoryBsonClassMapping.Apply();
            ProductBsonClassMapping.Apply();

            return services;
        }
    }
}
