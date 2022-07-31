using BuildingBlocks.Domain;
using BuildingBlocks.Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Repositories
{
    public class RedisCommandRepository<T> : IRedisCommandRepository<T> where T : AggregateBase
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RedisCommandRepository(IConnectionMultiplexer redis, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<T> AddAsync(T entity)
        {
            var data = JsonConvert.SerializeObject(entity);

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            await _database.StringSetAsync($"{tenant}_{typeof(T).Name.ToLower()}:{entity.Id}", data);
            return entity;
        }

        public Task<IReadOnlyCollection<T>> AddManyAsync(IReadOnlyCollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> PatchAsync(T entity)
        {
            var data = JsonConvert.SerializeObject(entity);

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            await _database.StringSetAsync($"{tenant}_{typeof(T).Name.ToLower()}:{entity.Id}", data);
            return entity;
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            var data = JsonConvert.SerializeObject(entity);

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            await _database.StringSetAsync($"{tenant}_{typeof(T).Name.ToLower()}:{id}", data);
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            await _database.KeyDeleteAsync($"{tenant}_{typeof(T).Name.ToLower()}:{entity.Id}");
        }
    }
}
