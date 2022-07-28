using BuildingBlocks.Domain;
using BuildingBlocks.Redis.Repositories.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Repositories
{
    public class RedisRepository<T> : IRedisRepository<T> where T : AggregateBase
    {
        private readonly IRedisCommandRepository<T> _commandRepository;
        private readonly IRedisQueryRepository<T> _queryRepository;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            _commandRepository = new RedisCommandRepository<T>(redis);
            _queryRepository = new RedisQueryRepository<T>(redis);
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _commandRepository.AddAsync(entity);
        }

        public Task<IReadOnlyCollection<T>> AddManyAsync(IReadOnlyCollection<T> entities)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
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

        public async Task RemoveAsync(T entity)
        {
            await _commandRepository.RemoveAsync(entity);
        }
    }
}
