using BuildingBlocks.Redis.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Core.Domain
{
    public interface IBasketRepository : IRedisRepository<Basket>
    {

    }
}
