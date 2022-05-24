using BuildingBlocks.Domain;
using BuildingBlocks.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Infrastructure.Data;

namespace TenantService.Infrastructure.Repositories
{
    public class TenantRepository<T> : Repository<T>, ITenantRepository<T> where T : AggregateRoot
    {
        public TenantRepository(TenantContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }
    }
}
