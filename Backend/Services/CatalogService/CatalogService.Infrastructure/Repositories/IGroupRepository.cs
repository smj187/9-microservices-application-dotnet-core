using BuildingBlocks.Mongo;
using CatalogService.Core.Entities.Aggregates;

namespace CatalogService.Infrastructure.Repositories
{
    public interface IGroupRepository : IMongoRepository<Group>
    {

    }
}
