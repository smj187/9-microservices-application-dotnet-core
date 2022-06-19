
using BuildingBlocks.Mongo.Repositories.Interfaces;

namespace CatalogService.Core.Domain.Products
{
    public interface IProductRepository : IMongoRepository<Product>
    {

    }
}
