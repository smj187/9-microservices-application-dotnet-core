using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo.Repositories
{
    public class MongoQueryRepository<T> : IMongoQueryRepository<T> where T : AggregateBase
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoQueryRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            var pl = new EnglishPluralizationService();
            var collectionName = pl.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task<T?> FindAsync(string id)
        {
            var filter = _filterBuilder.Eq(x => x.Id.ToString(), id);
            return await _mongoCollection.Find(filter).FirstOrDefaultAsync();
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

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).ToListAsync();
        }
    }
}
