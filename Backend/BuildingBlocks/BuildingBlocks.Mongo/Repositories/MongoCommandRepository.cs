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
    public class MongoCommandRepository<T> : IMongoCommandRepository<T> where T : AggregateBase
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoCommandRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            var pl = new EnglishPluralizationService();
            var collectionName = pl.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _mongoCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IReadOnlyCollection<T>> AddManyAsync(IReadOnlyCollection<T> entities)
        {
            await _mongoCollection.InsertManyAsync(entities);
            return entities;
        }

        public async Task<T> PatchAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = _filterBuilder.Eq(x => x.Id, entity.Id);
            await _mongoCollection.ReplaceOneAsync(filter, entity);
            return entity;
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

        public async Task PatchMultipleAsync(IReadOnlyCollection<WriteModel<T>> bulk)
        {
            await _mongoCollection.BulkWriteAsync(bulk);
        }
    }
}
