using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.v1.Commands
{
    public record DeliveryCommand(Guid CorrelationId, string TenantId, Guid OrderId, Guid UserId, List<Guid> Products, List<Guid> Sets);
}
