using BuildingBlocks.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Mongo
{
    public class MongoQueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoQueryRepository(IConfiguration configuration)
        {
            var connectionStr = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            var databaseName = configuration.GetValue<string>("ConnectionStrings:Database");

            var client = new MongoClient(connectionStr);
            var database = client.GetDatabase(databaseName);

            var pl = new EnglishPluralizationService();
            var collectionName = pl.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> FindAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).FirstOrDefaultAsync();
        }

        public Task<T> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            var filter = _filterBuilder.In(x => x.Id, includes);
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
