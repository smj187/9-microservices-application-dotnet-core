using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Mongo
{
    public interface IMongoRepository<T> : IBaseRepository<T> where T : IAggregateRoot
    {

    }
}
