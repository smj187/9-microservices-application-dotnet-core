using CatalogService.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public interface IQueryRepository<T> where T : IMongoEntity
    {
        Task<IReadOnlyCollection<T>> ListAsync();
    }
}
