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
        public static OrderStatus Created = new(0, "Order Created");
        public static OrderStatus ItemsAllocated = new(1, "All Items Are Allocated");
        public static OrderStatus ItemsOutOfStock = new(2, "Items Out Of Stock");
        public static OrderStatus ItemNotVisible = new(3, "Items Out Of Stock");
        public static OrderStatus PaymentSuccess = new(4, "Payment Success");
        public static OrderStatus PaymentFailure = new(5, "Payment Failure");
        public static OrderStatus TenantApproved = new(6, "Tenant Approved");
        public static OrderStatus TenantRejected = new(7, "Tenant Rejected");
        public static OrderStatus DeliverySuccess = new(8, "Delivery Success");
        public static OrderStatus DeliveryFailed = new(9, "Delivery Failied");

        public OrderStatus(int value, string description)
            : base(value, description)
        {

        }
    }
}
