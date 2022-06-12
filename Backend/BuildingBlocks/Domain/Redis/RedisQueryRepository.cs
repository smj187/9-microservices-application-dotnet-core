using BuildingBlocks.Domain.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Redis
{
    public class RedisQueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisQueryRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<T?> FindAsync(Guid id)
        {
            var response = await _database.StringGetAsync($"{typeof(T).Name.ToLower()}:{id}");
            return JsonConvert.DeserializeObject<T>(response);
        }

        public Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<T?> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            var endpoint = _redis.GetEndPoints();
            var server = _redis.GetServer(endpoint.First());
            var keys = server.Keys().Select(x => x.ToString()).Where(x => x.Contains(typeof(T).Name.ToLower()));


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
