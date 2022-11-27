using BuildingBlocks.Domain;
using BuildingBlocks.Mongo.Helpers;
using BuildingBlocks.Mongo.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using PluralizeService.Core;
using System;
using System.Collections.Generic;
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

            var collectionName = PluralizationProvider.Pluralize(typeof(T).Name.ToLower());

            _mongoCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<long> CountAsync()
        {
            return await _mongoCollection.CountDocumentsAsync(new BsonDocument());
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).FirstOrDefaultAsync() != null;
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

        public async Task<T> FindAsync(FilterDefinition<T> filter)
        {
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

        public async Task<IReadOnlyCollection<T>> ListAsync(FilterDefinition<T> filter)
        {
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public async Task<(MongoPaginationResult mongoPaginationResult, IReadOnlyCollection<T>)> ListAsync(int page, int pageSize)
        {
            var pagination = new MongoPaginationResult((int)await CountAsync(), page, pageSize);


            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<T, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<T>()
                }));

            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<T, T>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Skip<T>((page - 1) * pageSize),
                    PipelineStageDefinitionBuilder.Limit<T>(pageSize),
                }));

            var aggregation = await _mongoCollection.Aggregate()
                .Match(Builders<T>.Filter.Empty)
                .Facet(countFacet, dataFacet)
                .ToListAsync();


            var data = aggregation.First().Facets.First(x => x.Name == "data").Output<T>();

            return (pagination, data);
        }
    }
}
