using MongoDB.Bson.Serialization;
using OrderService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.BsonClassMapDefinitions.BsonClassMappings
{
    public static class OrderBsonClassMapping
    {
        public static void Apply()
        {
            BsonClassMap.RegisterClassMap<Order>(x =>
            {
                x.MapProperty(x => x.TenantId).SetElementName("tenant_id").SetIsRequired(true);
                x.MapProperty(x => x.UserId).SetElementName("user_id").SetIsRequired(true);
                x.MapProperty(x => x.Products).SetElementName("products").SetIsRequired(true);
                x.MapProperty(x => x.Sets).SetElementName("sets").SetIsRequired(true);
                x.MapProperty(x => x.OrderStatusValue).SetElementName("order_status_value").SetIsRequired(true);
                x.MapProperty(x => x.OrderStatusDescription).SetElementName("order_status_description").SetIsRequired(true);
                x.MapProperty(x => x.TotalAmount).SetElementName("total_amount").SetIsRequired(true);
            });
        }
    }
}
