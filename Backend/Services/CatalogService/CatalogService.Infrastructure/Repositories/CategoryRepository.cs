using BuildingBlocks.Domain.Mongo;
using CatalogService.Core.Domain.Categories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public class CategoryRepository : MongoRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}
