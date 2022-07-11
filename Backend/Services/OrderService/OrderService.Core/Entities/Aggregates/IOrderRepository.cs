using BuildingBlocks.Mongo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.Entities.Aggregates
{
    public interface IOrderRepository : IMongoRepository<Order>
    {

    }
}
