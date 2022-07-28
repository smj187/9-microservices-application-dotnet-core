using BuildingBlocks.Domain;
using BuildingBlocks.EfCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Repositories
{
    public class EfCommandRepository<T> : IEfCommandRepository<T> where T : AggregateBase
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

        public Task<IReadOnlyCollection<T>> AddManyAsync(IReadOnlyCollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> PatchAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _database.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            return await Task.FromResult(entity);
        }

        public Task<T> PatchAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
