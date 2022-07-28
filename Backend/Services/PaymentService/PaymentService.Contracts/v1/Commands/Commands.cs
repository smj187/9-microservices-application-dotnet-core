using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Commands
{
    public class PaymentProcessorCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }

}
