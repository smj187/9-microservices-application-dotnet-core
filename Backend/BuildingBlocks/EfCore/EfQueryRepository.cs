using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore
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

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _database.AsNoTracking().ToListAsync();
        }
    }
}
