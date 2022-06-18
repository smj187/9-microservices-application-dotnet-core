using BuildingBlocks.Domain.Mongo;
using CatalogService.Core.Domain.Products;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }

        public async Task<IEnumerable<Product>> UpdateMultipleQuantities(IEnumerable<Product> products)
        {
            var bulk = new List<WriteModel<Product>>();

            foreach (var product in products)
            {
                var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
                var update = Builders<Product>.Update.Set(x => x.Quantity, product.Quantity);

                var upsert = new UpdateOneModel<Product>(filter, update) { IsUpsert = false };
                bulk.Add(upsert);
            }

            await BulkWrite(bulk);

            return products;
        }
    }
}
