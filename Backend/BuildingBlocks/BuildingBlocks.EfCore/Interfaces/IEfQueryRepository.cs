using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using BuildingBlocks.EfCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Interfaces
{
    public interface IEfQueryRepository<T> : IQueryRepository<T> where T : IAggregateBase
    {
        Task<long> CountAsync();
        Task<PagedResult<T>> ListAsync(int page, int pageSize);
    }
}
