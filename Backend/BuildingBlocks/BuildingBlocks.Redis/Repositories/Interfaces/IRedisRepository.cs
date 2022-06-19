using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Repositories.Interfaces
{
    public interface IRedisRepository<T> : IRedisCommandRepository<T>, IRedisQueryRepository<T> where T : AggregateBase
    {

    }
}
