using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.Entities.Enumerations
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus OrderCreatedStatus = new(0, "Order created, awaiting catalog's product and set allocation");

        public static OrderStatus CatalogProductsAndSetsAllocationSuccess = new(1, "catalog products and sets allocation success");
        public static OrderStatus CatalogProductsAndSetsOutOfStockFailure = new(2, "catalog products and sets out of stock failure");
        public static OrderStatus CatalogProductsAndSetsUnavailableFailure = new(3, "catalog products and sets unavailable failure");

        public static OrderStatus PaymentProcessSuccess = new(4, "payment process success");
        public static OrderStatus PaymentProcessFailure = new(5, "payment process failure");

        public static OrderStatus TenantApproved = new(6, "Tenant approved, awaiting delivery");
        public static OrderStatus TenantRejected = new(7, "Tenant Rejected");

        public static OrderStatus DeliverySuccess = new(8, "Delivery complete, order finished");
        public static OrderStatus DeliveryFailed = new(9, "Delivery Failied");

        public static OrderStatus OrderComplete = new(10, "Order paid, approved and delivered - [complete]");

        public OrderStatus(int value, string description)
            : base(value, description)
        {

        }

  

        public static OrderStatus Create(int value)
        {
            if (value == 0) return new OrderStatus(OrderCreatedStatus.Value, OrderCreatedStatus.Description);

            if (value == 1) return new OrderStatus(CatalogProductsAndSetsAllocationSuccess.Value, CatalogProductsAndSetsAllocationSuccess.Description);
            if (value == 2) return new OrderStatus(CatalogProductsAndSetsOutOfStockFailure.Value, CatalogProductsAndSetsOutOfStockFailure.Description);
            if (value == 3) return new OrderStatus(CatalogProductsAndSetsUnavailableFailure.Value, CatalogProductsAndSetsUnavailableFailure.Description);

            if (value == 4) return new OrderStatus(PaymentProcessSuccess.Value, PaymentProcessSuccess.Description);
            if (value == 5) return new OrderStatus(PaymentProcessFailure.Value, PaymentProcessFailure.Description);

            if (value == 6) return new OrderStatus(TenantApproved.Value, TenantApproved.Description);
            if (value == 7) return new OrderStatus(TenantRejected.Value, TenantRejected.Description);

            if (value == 8) return new OrderStatus(DeliverySuccess.Value, DeliverySuccess.Description);
            if (value == 9) return new OrderStatus(DeliveryFailed.Value, DeliveryFailed.Description);

            if (value == 10) return new OrderStatus(OrderComplete.Value, OrderComplete.Description);

            throw new NotImplementedException();
        }
    }
}
