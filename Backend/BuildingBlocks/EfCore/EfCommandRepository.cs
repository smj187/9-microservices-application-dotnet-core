using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore
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

        public async Task AddAsync(T entity)
        {
            await _database.AddAsync(entity);
        }
    }
}
