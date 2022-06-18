using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Products
{
    public interface IProductRepository : IMongoRepository<Product>
    {
        Task<IEnumerable<Product>> UpdateMultipleQuantities(IEnumerable<Product> products);
    }
}
