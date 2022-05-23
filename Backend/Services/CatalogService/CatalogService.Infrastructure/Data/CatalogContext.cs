using CatalogService.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Data
{
    public class CatalogContext : ICatelogContext
    {
        public CatalogContext(IConfiguration config)
        {
            var str = config.GetValue<string>("ConnectionStrings:DefaultConnection");
            var client = new MongoClient(str);
            var db = config.GetValue<string>("ConnectionStrings:Database");
            var database = client.GetDatabase(db);

            Products = database.GetCollection<Product>("products");
            Categories = database.GetCollection<Category>("categories");
        }

        public IMongoCollection<Product> Products { get; private set; }
        public IMongoCollection<Category> Categories { get; private set; }
    }
}
