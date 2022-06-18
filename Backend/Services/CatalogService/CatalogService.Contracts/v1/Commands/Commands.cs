using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Commands
{
    public record CatalogAllocationCommand(Guid CorrelationId, Guid OrderId, List<Guid> Products, List<Guid> Sets);
}
