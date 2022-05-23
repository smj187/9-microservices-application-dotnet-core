using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Core.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public Guid OrderId { get; set; }
    }
}
