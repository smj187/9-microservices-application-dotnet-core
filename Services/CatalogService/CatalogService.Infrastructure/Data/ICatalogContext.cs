using CatalogService.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }

        IMongoCollection<Category> Categories { get; }
    }
}
