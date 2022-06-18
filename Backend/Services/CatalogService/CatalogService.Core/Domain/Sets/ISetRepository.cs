using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Sets
{
    public interface ISetRepository : IMongoRepository<Set>
    {
        Task<IEnumerable<Set>> UpdateMultipleQuantities(IEnumerable<Set> products);
    }
}
