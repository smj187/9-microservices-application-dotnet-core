using BuildingBlocks.Domain.Repositories;
using MongoDB.Driver;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Redis
{
    public class RedisCommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCommandRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<T> AddAsync(T entity)
        {
            var data = JsonConvert.SerializeObject(entity);

            await _database.StringSetAsync($"{typeof(T).Name.ToLower()}:{entity.Id}", data);
            return entity;
        }

        public Task BulkWrite(IEnumerable<WriteModel<T>> bulk)
        {
            throw new NotImplementedException();
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            var data = JsonConvert.SerializeObject(entity);

            await _database.StringSetAsync($"{typeof(T).Name.ToLower()}:{entity.Id}", data);
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            await _database.KeyDeleteAsync($"{typeof(T).Name.ToLower()}:{entity.Id}");
        }
    }
}
