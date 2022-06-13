using BuildingBlocks.Domain.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Core.Domain.Aggregates
{
    public interface ITenantRepository : IEfRepository<Tenant>
    {

    }
}
