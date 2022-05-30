using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Mongo
{
    public interface IMongoRepository<T> : IBaseRepository<T> where T : IAggregateRoot
    {
        Task AddAsync(T entity);
        Task<T> FindAsync(Guid id);
        Task<T> FindAsync(Expression<Func<T, bool>> expression);
        Task<IReadOnlyCollection<T>> FindAsync(List<Guid> includes);
        Task<T> PatchAsync(Guid id, T entity);
    }
}
