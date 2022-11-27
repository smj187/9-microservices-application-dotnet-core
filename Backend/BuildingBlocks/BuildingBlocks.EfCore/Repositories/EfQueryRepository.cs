using BuildingBlocks.Domain;
using BuildingBlocks.EfCore.Helpers;
using BuildingBlocks.EfCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BuildingBlocks.EfCore.Repositories
{
    public class EfQueryRepository<T> : IEfQueryRepository<T> where T : AggregateBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _database;

        public EfQueryRepository(DbContext context)
        {
            _context = context;
            _database = _context.Set<T>();
        }

        public async Task<long> CountAsync()
        {
            return await _database.CountAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _database.AnyAsync(expression);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            // https://stackoverflow.com/questions/41025338/why-use-attach-for-update-entity-framework-6
            return await _database.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> FindAsync(string id)
        {
            return await _database.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _database.FirstOrDefaultAsync(expression);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _database.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            return await _database.Where(x => includes.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _database.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<PagedResult<T>> ListAsync(int page, int pageSize)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = await _database.CountAsync()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = _database.AsNoTracking().Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
