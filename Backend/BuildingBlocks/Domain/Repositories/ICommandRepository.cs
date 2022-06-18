using BuildingBlocks.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.Repositories
{
    public interface ICommandRepository<T> where T : IAggregateRoot
    {
        Task<T> AddAsync(T entity);

        Task<T> PatchAsync(Guid id, T entity);


        Task BulkWrite(IEnumerable<WriteModel<T>> bulk);
        Task RemoveAsync(T entity);

    }
}
