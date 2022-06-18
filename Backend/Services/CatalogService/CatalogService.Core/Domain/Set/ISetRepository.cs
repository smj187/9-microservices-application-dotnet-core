using BuildingBlocks.Domain.Mongo;

namespace CatalogService.Core.Domain.Set
{
    public interface ISetRepository : IMongoRepository<Set>
    {
        Task<IEnumerable<Set>> UpdateMultipleQuantities(IEnumerable<Set> products);
    }
}
