using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings;

namespace TranslationService.Infrastructure.BsonClassMapDefinitions
{
    public static class MongoDbMapperConfiguration
    {
        public static IServiceCollection AddBsonClassMappings(this IServiceCollection services)
        {
            TranslationsBsonClassMapping.Apply();

            return services;
        }
    }
}
