﻿using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Repositories.Interfaces
{
    public interface IMongoQueryRepository<T> : IQueryRepository<T> where T : IAggregateBase
    {
        Task<T> FindAsync(FilterDefinition<T> filter);
        Task<IReadOnlyCollection<T>> ListAsync(FilterDefinition<T> filter);
    }
}
