
using BuildingBlocks.Mongo.Repositories.Interfaces;

namespace CatalogService.Core.Domain.Categories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {

    }
}
