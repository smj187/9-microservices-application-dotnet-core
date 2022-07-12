using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Contracts.v1.Contracts
{
    public record PaymentResponse(Guid UserId, Guid TenantId, Guid OrderId, bool Success);
}
