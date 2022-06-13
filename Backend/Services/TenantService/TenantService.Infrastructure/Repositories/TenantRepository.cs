using BuildingBlocks.Domain.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;

namespace TenantService.Infrastructure.Repositories
{
    public class TenantRepository : EfRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(TenantContext context) 
            : base(context)
        {

        }
    }
}
