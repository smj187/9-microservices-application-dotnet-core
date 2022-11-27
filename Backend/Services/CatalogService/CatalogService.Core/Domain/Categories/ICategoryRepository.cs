using BuildingBlocks.Mongo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Categories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {

    }
}