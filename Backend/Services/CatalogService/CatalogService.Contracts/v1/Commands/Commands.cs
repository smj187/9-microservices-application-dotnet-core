using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Commands
{
    public record CatalogSagaAllocationCommand(Guid CorrelationId, string TenantId, Guid OrderId, List<Guid> Products, List<Guid> Sets);
}
