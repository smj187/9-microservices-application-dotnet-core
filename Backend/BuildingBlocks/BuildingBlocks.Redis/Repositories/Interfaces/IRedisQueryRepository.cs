using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Repositories.Interfaces
{
    public interface IRedisQueryRepository<T> : IQueryRepository<T> where T : IAggregateBase
    {

    }
}
