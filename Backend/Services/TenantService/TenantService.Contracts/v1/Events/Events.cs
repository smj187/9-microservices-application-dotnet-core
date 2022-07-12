using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Contracts.v1.Events
{
    public record TenantApproveOrderSagaEvent(Guid CorrelationId, string? Message = null);
    public record TenantRejectOrderSagaEvent(Guid CorrelationId, string? Message = null);
}
