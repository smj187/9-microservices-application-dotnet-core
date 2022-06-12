using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Redis
{
    public interface IRedisRepository<T> : IBaseRepository<T> where T : IAggregateRoot
    {

    }
}
