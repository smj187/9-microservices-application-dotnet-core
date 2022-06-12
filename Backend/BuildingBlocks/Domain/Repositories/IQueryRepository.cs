using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Repositories
{
    public interface IQueryRepository<T> where T : IAggregateRoot
    {
        Task<IReadOnlyCollection<T>> ListAsync();
        Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes);
        Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression);

        Task<T?> FindAsync(Guid id);
        Task<T?> FindAsync(string id);
        Task<T?> FindAsync(Expression<Func<T, bool>> expression);
    }
}
