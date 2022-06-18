using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Categories
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {

    }
}
