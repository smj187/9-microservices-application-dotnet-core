using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Contracts.v1.Events
{
    public record TenantApproveOrderEvent(Guid CorrelationId, Guid OrderId, string? Message);
    public record TenantRejectOrderEvent(Guid CorrelationId, Guid OrderId, string? Message);
}
