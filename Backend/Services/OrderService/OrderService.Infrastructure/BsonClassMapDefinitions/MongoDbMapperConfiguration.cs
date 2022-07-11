using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.BsonClassMapDefinitions
{
    public static class MongoDbMapperConfiguration
    {
        public static IServiceCollection AddBsonClassMappings(this IServiceCollection services)
        {
            OrderBsonClassMapping.Apply();

            return services;
        }
    }
}
