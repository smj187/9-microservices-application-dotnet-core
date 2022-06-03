using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Category
{
    public interface ICategoryRepository : IMongoRepository<Category>
    {

    }
}
