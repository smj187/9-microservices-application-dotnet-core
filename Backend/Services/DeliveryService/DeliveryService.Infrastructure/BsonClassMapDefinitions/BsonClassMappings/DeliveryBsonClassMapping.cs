using DeliveryService.Core.Domain.Aggregates;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public static class DeliveryBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Delivery>(x =>
            {
                x.MapProperty(x => x.TenantId).SetElementName("tenant_id").SetIsRequired(true);
                x.MapProperty(x => x.Products).SetElementName("products").SetIsRequired(true);
                x.MapProperty(x => x.Sets).SetElementName("sets").SetIsRequired(true);
                x.MapProperty(x => x.DeliveryStatusValue).SetElementName("delivery_status_value").SetIsRequired(true);
                x.MapProperty(x => x.DeliveryStatusDescription).SetElementName("delivery_status_description").SetIsRequired(true);
                x.MapProperty(x => x.OrderId).SetElementName("order_id").SetIsRequired(true);
                x.MapProperty(x => x.UserId).SetElementName("user_id").SetIsRequired(true);
            });
        }
    }
}
