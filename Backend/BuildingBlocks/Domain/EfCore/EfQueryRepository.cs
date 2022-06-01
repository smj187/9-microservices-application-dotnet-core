using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.EfCore
{
    public class EfQueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _database;

        public EfQueryRepository(DbContext context)
        {
            _context = context;
            _database = _context.Set<T>();
        }

        public Task<T> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<T>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            throw new NotImplementedException();
        }
    }
}
