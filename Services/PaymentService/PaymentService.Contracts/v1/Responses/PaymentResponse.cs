using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Responses
{
    public class PaymentResponse
    {
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public Guid OrderId { get; set; }

        public bool Success { get; set; }
    }
}
