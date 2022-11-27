using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Interfaces
{
    public interface IMongoRepository<T> : IMongoCommandRepository<T>, IMongoQueryRepository<T> where T : IAggregateBase
    {

    }
}