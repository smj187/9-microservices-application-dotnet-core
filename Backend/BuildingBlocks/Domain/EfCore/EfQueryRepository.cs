using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<T?> FindAsync(Guid id)
        {
            return await _database.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _database.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public Task<T?> FindAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _database.AsNoTracking().ToListAsync();
        }

        public Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _database.AsNoTracking().Where(expression).ToListAsync();
        }
    }
}
