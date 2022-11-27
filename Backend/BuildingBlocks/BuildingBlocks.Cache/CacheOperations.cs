using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Cache
{
    public static class CacheOperations
    {
        public static void ClearCacheByPattern(this IDistributedCache distributedCache, IConfiguration configuration, string pattern)
        {
            var server = configuration.GetValue<string>("Cache:Server");
            var port = configuration.GetValue<string>("Cache:Port");
            var connectionMultiplexer = ConnectionMultiplexer.Connect($"{server}:{port}");

            var endPoint = connectionMultiplexer.GetEndPoints().First();

            foreach (var key in connectionMultiplexer.GetServer(endPoint).Keys(pattern: pattern))
            {
                distributedCache.Remove(key);
            }

            connectionMultiplexer.Close();
        }
    }
}
