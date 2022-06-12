using BasketService.Contracts.v1.Contracts;
using BasketService.Core.Domain;
using BuildingBlocks.Domain.Redis;
using BuildingBlocks.Domain.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasketService.Infrastructure.Repositories
{
    public class BasketRepository : RedisRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IConnectionMultiplexer redis) 
            : base(redis)
        {

        }
    }
}
