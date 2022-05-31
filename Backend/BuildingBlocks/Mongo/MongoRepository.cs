using BuildingBlocks.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IAggregateRoot
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoRepository(IConfiguration configuration)
        {
            var connectionStr = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            var databaseName = configuration.GetValue<string>("ConnectionStrings:Database");

            var client = new MongoClient(connectionStr);
            var database = client.GetDatabase(databaseName);

            var pl = new EnglishPluralizationService();
            var collectionName = pl.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _mongoCollection.InsertOneAsync(entity);
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

        public async Task<IReadOnlyCollection<T>> FindAsync(List<Guid> includes)
        {
            var filter = _filterBuilder.In(x => x.Id, includes);
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = _filterBuilder.Eq(x => x.Id, id);
            await _mongoCollection.ReplaceOneAsync(filter, entity);
            return entity;
        }
    }
}
