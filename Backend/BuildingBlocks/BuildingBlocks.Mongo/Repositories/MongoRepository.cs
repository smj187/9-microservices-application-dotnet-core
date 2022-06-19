using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T> where T : AggregateBase
    {
        private readonly IMongoCommandRepository<T> _commandRepository;
        private readonly IMongoQueryRepository<T> _queryRepository;

        public MongoRepository(IConfiguration configuration)
        {
            _commandRepository = new MongoCommandRepository<T>(configuration);
            _queryRepository = new MongoQueryRepository<T>(configuration);
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _commandRepository.AddAsync(entity);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public async Task<T?> FindAsync(string id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.FindAsync(expression);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _queryRepository.ListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            return await _queryRepository.ListAsync(includes);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.ListAsync(expression);
        }

        public async Task<T> PatchAsync(T entity)
        {
            return await _commandRepository.PatchAsync(entity);
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            return await _commandRepository.PatchAsync(id, entity);
        }

        public async Task PatchMultipleAsync(IReadOnlyCollection<WriteModel<T>> bulk)
        {
            await _commandRepository.PatchMultipleAsync(bulk);
        }
    }
}
