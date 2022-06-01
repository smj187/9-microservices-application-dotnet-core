using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.EfCore
{
    public class EfCommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _database;

        public EfCommandRepository(DbContext context)
        {
            _context = context;
            _database = _context.Set<T>();
        }

        public Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> PatchAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
