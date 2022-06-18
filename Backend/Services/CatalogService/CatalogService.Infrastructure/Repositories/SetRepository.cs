using BuildingBlocks.Domain.Mongo;
using CatalogService.Core.Domain.Set;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class SetRepository : MongoRepository<Set>, ISetRepository
    {
        public SetRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }

        public async Task<IEnumerable<Set>> UpdateMultipleQuantities(IEnumerable<Set> products)
        {
            var bulk = new List<WriteModel<Set>>();

            foreach (var product in products)
            {
                var filter = Builders<Set>.Filter.Eq(x => x.Id, product.Id);
                var update = Builders<Set>.Update.Set(x => x.Quantity, product.Quantity);

                var upsert = new UpdateOneModel<Set>(filter, update) { IsUpsert = false };
                bulk.Add(upsert);
            }

            await BulkWrite(bulk);

            return products;
        }
    }
}
