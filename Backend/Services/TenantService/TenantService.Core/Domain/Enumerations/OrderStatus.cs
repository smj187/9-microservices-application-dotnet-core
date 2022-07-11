using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Core.Domain.Enumerations
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Created = new(0, "Order created");

        public static OrderStatus Accepted = new(1, "Order accepted");
        public static OrderStatus Rejected = new(2, "Order rejected");

        public OrderStatus(int value, string description)
            : base(value, description)
        {

        }

        public static OrderStatus Create(int value)
        {
            if (value == 0) return new OrderStatus(Created.Value, Created.Description);

            if (value == 1) return new OrderStatus(Accepted.Value, Accepted.Description);
            if (value == 2) return new OrderStatus(Rejected.Value, Rejected.Description);

            throw new NotImplementedException();
        }
    }
}
