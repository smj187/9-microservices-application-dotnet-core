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
        public static OrderStatus Created = new(0, "Order created, awaiting catalog's product and set allocation");

        public static OrderStatus ItemsAllocated = new(1, "Products and sets allocated, awaiting payment");
        public static OrderStatus ItemsOutOfStock = new(2, "Items Out Of Stock");
        public static OrderStatus ItemNotVisible = new(3, "Items Out Of Stock");

        public static OrderStatus PaymentSuccess = new(4, "Payment success, awaiting tenant approval");
        public static OrderStatus PaymentFailure = new(5, "Payment Failure");

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
            if (value == 0) return new OrderStatus(Created.Value, Created.Description);

            if (value == 1) return new OrderStatus(ItemsAllocated.Value, ItemsAllocated.Description);
            if (value == 2) return new OrderStatus(ItemsOutOfStock.Value, ItemsOutOfStock.Description);
            if (value == 3) return new OrderStatus(ItemNotVisible.Value, ItemNotVisible.Description);

            if (value == 4) return new OrderStatus(PaymentSuccess.Value, PaymentSuccess.Description);
            if (value == 5) return new OrderStatus(PaymentFailure.Value, PaymentFailure.Description);

            if (value == 6) return new OrderStatus(TenantApproved.Value, TenantApproved.Description);
            if (value == 7) return new OrderStatus(TenantRejected.Value, TenantRejected.Description);

            if (value == 8) return new OrderStatus(DeliverySuccess.Value, DeliverySuccess.Description);
            if (value == 9) return new OrderStatus(DeliveryFailed.Value, DeliveryFailed.Description);

            if (value == 10) return new OrderStatus(OrderComplete.Value, OrderComplete.Description);

            throw new NotImplementedException();
        }
    }
}
