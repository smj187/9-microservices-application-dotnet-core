using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.v1.Responses
{
    public class DeliveryReponse
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
