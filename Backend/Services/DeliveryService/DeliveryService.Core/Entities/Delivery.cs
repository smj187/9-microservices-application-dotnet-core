using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Core.Entities
{
    public class Delivery : AggregateBase
    {
        public Guid OrderId { get; set; }

        public Guid TenantId { get; set; }

        public Guid UserId { get; set; }
    }
}
