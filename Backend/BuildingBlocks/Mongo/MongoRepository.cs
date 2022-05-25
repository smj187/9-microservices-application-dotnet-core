using BuildingBlocks.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IAggregateRoot
    {
        private readonly IMongoCollection<T> _mongoCollection;
        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoRepository(string connectionStr, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionStr);
            var database = client.GetDatabase(databaseName);

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _mongoCollection.Find(_filterBuilder.Empty).ToListAsync();
        }
    }
}
