using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Repositories.Categories
{
    public class CategoryCommandRepository : CommandRepository<Category>, ICategoryCommandRepository
    {
        public CategoryCommandRepository(IConfiguration config, string collection)
            : base(config, collection)
        {

        }
    }
}
