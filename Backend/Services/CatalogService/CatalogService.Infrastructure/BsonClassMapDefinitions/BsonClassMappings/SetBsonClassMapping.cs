using CatalogService.Core.Domain.Sets;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public static class SetBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Set>(x =>
            {
                x.MapProperty(x => x.TenantId).SetElementName("tenant_id").SetIsRequired(true);
                x.MapProperty(x => x.Name).SetElementName("name").SetIsRequired(true);
                x.MapProperty(x => x.Description).SetElementName("description").SetIsRequired(false);
                x.MapProperty(x => x.IsVisible).SetElementName("is_visible").SetIsRequired(true);
                x.MapProperty(x => x.Price).SetElementName("price").SetSerializer(new DecimalSerializer(BsonType.Decimal128));
                x.MapProperty(x => x.PriceDescription).SetElementName("price_description").SetIsRequired(false);
                x.MapProperty(x => x.IsAvailable).SetElementName("is_available").SetIsRequired(true).SetDefaultValue(false);
                x.MapProperty(x => x.Quantity).SetElementName("quantity").SetIsRequired(false);
                x.MapProperty(x => x.Assets).SetElementName("assets").SetIsRequired(true);
                x.MapProperty(x => x.Tags).SetElementName("tags").SetIsRequired(true);
                x.MapProperty(x => x.Products).SetElementName("products").SetIsRequired(true);
            });
        }
    }
}
