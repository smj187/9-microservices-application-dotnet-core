using CatalogService.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastructure.Repositories
{
    public interface ICommandRepository<T> where T : IMongoEntity
    {
        Task<T> CreateAsync(T entity);
    }
}
