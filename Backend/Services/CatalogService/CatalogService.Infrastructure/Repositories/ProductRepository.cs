using BuildingBlocks.Domain.Mongo;
using CatalogService.Core.Domain.Product;
using Microsoft.Extensions.Configuration;
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
    }
}
