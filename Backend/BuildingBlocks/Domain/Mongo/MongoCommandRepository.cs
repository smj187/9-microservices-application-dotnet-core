using BuildingBlocks.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Mongo
{
    public class MongoCommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoCommandRepository(IConfiguration configuration)
        {
            var connectionStr = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            var databaseName = configuration.GetValue<string>("ConnectionStrings:Database");

            var client = new MongoClient(connectionStr);
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

     
        public async Task BulkWrite(IEnumerable<WriteModel<T>> bulk)
        {
            await _mongoCollection.BulkWriteAsync(bulk);
        }

        public Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
