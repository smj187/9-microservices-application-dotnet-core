using BuildingBlocks.Domain.Interfaces;

namespace BuildingBlocks.Domain.Repositories
{
    public interface IRepository<T> : IBaseRepository<T> where T : IAggregateRoot
    {

    }
}