using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Interfaces
{
    public interface IRedisCommandRepository<T> : ICommandRepository<T> where T : IAggregateBase
    {
        Task RemoveAsync(T entity);
    }
}
