using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Commands
{
    public class CatalogProductsAndSetsAllocationCommand
    {
        public Guid CorrelationId { get; set; }
        public string TenantId { get; set; } = default!;
        public List<Guid> Products { get; set; } = new();
        public List<Guid> Sets { get; set; } = new();
    }

}
