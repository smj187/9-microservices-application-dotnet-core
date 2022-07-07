using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Multitenancy.Configurations
{
    public class TenantConfiguration
    {
        public string Name { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        public string ConnectionString { get; set; } = default!;
    }
}
