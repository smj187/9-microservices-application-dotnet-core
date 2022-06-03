using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Product
{
    public interface IProductRepository : IMongoRepository<Product>
    {

    }
}
