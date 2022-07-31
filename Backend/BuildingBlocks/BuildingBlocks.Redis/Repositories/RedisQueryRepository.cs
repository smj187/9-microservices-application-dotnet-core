using BuildingBlocks.Domain;
using BuildingBlocks.Redis.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Redis.Repositories
{
    public class RedisQueryRepository<T> : IRedisQueryRepository<T> where T : AggregateBase
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDatabase _database;

        public RedisQueryRepository(IConnectionMultiplexer redis, IHttpContextAccessor httpContextAccessor)
        {
            _redis = redis;
            _httpContextAccessor = httpContextAccessor;
            _database = _redis.GetDatabase();
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<T?> FindAsync(Guid id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            var response = await _database.StringGetAsync($"{tenant}_{typeof(T).Name.ToLower()}:{id}");
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T?> FindAsync(string id)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            var response = await _database.StringGetAsync($"{tenant}_{typeof(T).Name.ToLower()}:{id}");
            return JsonConvert.DeserializeObject<T>(response);
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            var endpoint = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoint.First());

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new NullReferenceException($"http context accessor is not defined");
            }

            var tenant = context.Request.Headers["tenant-id"].ToString().ToLower();

            var a = server.Keys();
            var b = a.Select(x => x.ToString());
            var c = b.Where(x => x.StartsWith(tenant)).ToList();

            var name = typeof(T).Name.ToLower();
            var keys = server.Keys().Select(x => x.ToString()).Where(x => x.StartsWith(tenant)).Where(x => x.Contains(name));


            var list = new List<T>();
            await Task.WhenAll(keys.Select(async key =>
            {
                var str = await _database.StringGetAsync(key);
                var mapped = JsonConvert.DeserializeObject<T>(str);
                if (mapped != null)
                {
                    list.Add(mapped);
                }
            }));

            return list;
        }

        public Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
