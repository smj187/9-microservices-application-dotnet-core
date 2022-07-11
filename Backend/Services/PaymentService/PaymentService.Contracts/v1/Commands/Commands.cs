using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Commands
{
    public record PaymentCommand(Guid CorrelationId, string TenantId, Guid OrderId, Guid UserId, decimal Amount, List<Guid> Products, List<Guid> Sets);
}
