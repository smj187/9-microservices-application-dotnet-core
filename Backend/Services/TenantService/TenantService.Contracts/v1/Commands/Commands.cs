using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Contracts.v1.Commands
{
    public record TenantCommand(Guid CorrelationId, Guid OrderId, Guid UserId, List<Guid> Items, decimal Amount);
}
