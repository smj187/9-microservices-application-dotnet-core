using BuildingBlocks.Mongo.Repositories;
using BuildingBlocks.Multitenancy.Interfaces.Services;
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
        public CategoryRepository(IMultitenancyService multitenancyService) 
            : base(multitenancyService.GetConnectionString(), $"catalog_{multitenancyService.GetTenantId()}")
        {

        }
    }
}
