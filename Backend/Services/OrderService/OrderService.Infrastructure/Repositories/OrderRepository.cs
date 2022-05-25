using BuildingBlocks.Domain;
using BuildingBlocks.EfCore;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository<T> : Repository<T>, IOrderRepository<T> where T : AggregateRoot
    {
        public OrderRepository(OrderContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }
    }
}
