using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
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

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _database.AddAsync(entity);
            return entity;
        }

        public Task BulkWrite(IEnumerable<WriteModel<T>> bulk)
        {
            throw new NotImplementedException();
        }

        public Task<T> PatchAsync(Guid id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _database.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return Task.FromResult(entity);
        }
    }
}
