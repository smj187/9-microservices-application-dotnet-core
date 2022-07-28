using BuildingBlocks.EfCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.Data;

namespace TenantService.Infrastructure.Repositories
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(TenantContext context) 
            : base(context)
        {

        }
    }
}
