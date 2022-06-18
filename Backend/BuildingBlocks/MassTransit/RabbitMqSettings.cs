using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit
{
    public class RabbitMqSettings
    {
        // generall
        public const string Host = "localhost";
        public const string VirtualHost = "/";
        public const string Username = "guest";
        public const string Password = "guest";

        // saga
        public const string OrderSagaName = "order_service_order_saga";
        public const string CatalogAllocationEndpointName = "catalog_service_consumer_item_allocation";
        public const string PaymentConsumerEndpointName = "payment_service_payment_process";
        public const string TenantConsumerEndpointName = "tenant_service_tenant_approval";
        public const string DeliveryConsumerEndpointName = "delivery_service_delivery_process";
        public const string CreateNewOrderFromBasket = "basket_service_create_new_order";







        public const string RabbitMqUri = "rabbitmq://localhost";
        public const string MediaCatalogQueue = "queue:media-catalog-queue";



    }
}
