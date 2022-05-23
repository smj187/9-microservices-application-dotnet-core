using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.v1.Requests
{
    public class CreateDelivery
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public Guid TenantId { get; set; }
    }
}
