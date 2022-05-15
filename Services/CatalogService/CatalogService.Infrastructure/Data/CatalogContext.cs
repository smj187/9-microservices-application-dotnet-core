using CatalogService.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoDatabase _database;

        public CatalogContext(string connectionStr, string database)
        {
            var client = new MongoClient(connectionStr);
            _database = client.GetDatabase(database);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("categories");
    }
}
