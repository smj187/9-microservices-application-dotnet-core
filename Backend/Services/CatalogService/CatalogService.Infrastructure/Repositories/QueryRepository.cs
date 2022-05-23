using CatalogService.Core.Entities.Base;
using CatalogService.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : IMongoEntity
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public QueryRepository(IConfiguration config, string collection)
        {
            var str = config.GetValue<string>("ConnectionStrings:DefaultConnection");
            var client = new MongoClient(str);

            var db = config.GetValue<string>("ConnectionStrings:Database");
            var database = client.GetDatabase(db);

            _mongoCollection = database.GetCollection<T>(collection);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }
    }
}
