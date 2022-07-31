using BasketService.Core.Domain;
using BuildingBlocks.Redis.Repositories;
using Microsoft.AspNetCore.Http;
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
        public BasketRepository(IConnectionMultiplexer redis, IHttpContextAccessor httpContextAccessor) 
            : base(redis, httpContextAccessor)
        {

        }
    }
}
