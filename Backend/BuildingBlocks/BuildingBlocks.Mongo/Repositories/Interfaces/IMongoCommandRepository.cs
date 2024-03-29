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
    public interface IMongoCommandRepository<T> : ICommandRepository<T> where T : IAggregateBase
    {
        Task PatchMultipleAsync(IReadOnlyCollection<WriteModel<T>> bulk);
    }
}
