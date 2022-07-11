using BuildingBlocks.Mongo.Repositories;
using BuildingBlocks.Mongo.Repositories.Interfaces;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using OrderService.Core.Entities.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : MongoRepository<Order>, IOrderRepository
    {
        public OrderRepository(IMultitenancyService multitenancyService)
            : base(multitenancyService.GetConnectionString(), $"orders_{multitenancyService.GetTenantId()}")
        {

        }
    }
}
