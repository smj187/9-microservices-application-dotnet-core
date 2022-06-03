using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Mongo
{
    public class MongoRepository<T> : BaseRepository<T>, IMongoRepository<T> where T : AggregateRoot
    {
        public MongoRepository(IConfiguration configuration)
            : base(new MongoCommandRepository<T>(configuration), new MongoQueryRepository<T>(configuration))
        {

        }
    }
}
