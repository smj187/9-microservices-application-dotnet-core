using BuildingBlocks.Domain.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Redis
{
    public class RedisRepository<T> : BaseRepository<T>, IRedisRepository<T> where T : AggregateRoot
    {
        public RedisRepository(IConnectionMultiplexer redis) 
            : base(new RedisCommandRepository<T>(redis), new RedisQueryRepository<T>(redis))
        {

        }
    }
}
