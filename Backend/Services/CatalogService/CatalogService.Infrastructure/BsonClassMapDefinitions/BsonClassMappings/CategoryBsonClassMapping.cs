using CatalogService.Core.Domain.Categories;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public static class CategoryBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Category>(x =>
            {
                x.MapProperty(x => x.TenantId).SetElementName("tenant_id").SetIsRequired(true);
                x.MapProperty(x => x.Name).SetElementName("name").SetIsRequired(true);
                x.MapProperty(x => x.Description).SetElementName("description").SetIsRequired(false);
                x.MapProperty(x => x.IsVisible).SetElementName("is_visible").SetIsRequired(true);
                x.MapProperty(x => x.Assets).SetElementName("assets");
                x.MapProperty(x => x.Products).SetElementName("products");
                x.MapProperty(x => x.Sets).SetElementName("sets");
            });
        }
    }
}