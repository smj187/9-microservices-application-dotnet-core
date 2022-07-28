using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.Aggregates;
using TranslationService.Core.ValueObjects;

namespace TranslationService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public static class TranslationsBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Translation>(x =>
            {
                x.MapProperty(x => x.TenantId).SetElementName("tenant_id").SetIsRequired(true);
                x.MapProperty(x => x.Service).SetElementName("service").SetIsRequired(true);
                x.MapProperty(x => x.Resource).SetElementName("resource").SetIsRequired(true);
                x.MapProperty(x => x.Identifier).SetElementName("identifier").SetIsRequired(true);
                x.MapProperty(x => x.Field).SetElementName("field").SetIsRequired(true);
                x.MapProperty(x => x.Multilinguals).SetElementName("multilinguals").SetIsRequired(true);
                x.MapProperty(x => x.Key).SetElementName("key").SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Multilingual>(x =>
            {
                x.MapProperty(x => x.Value).SetElementName("value").SetIsRequired(true);
                x.MapProperty(x => x.Locale).SetElementName("locale").SetIsRequired(true);
            });
        }
    }
}
