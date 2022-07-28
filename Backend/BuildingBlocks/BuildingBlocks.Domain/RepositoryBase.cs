using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : AggregateBase
    {
        private readonly ICommandRepository<T> _commandRepository;
        private readonly IQueryRepository<T> _queryRepository;

        public RepositoryBase(ICommandRepository<T> commandRepository, IQueryRepository<T> queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await _commandRepository.AddAsync(entity);
        }

        public async Task<IReadOnlyCollection<T>> AddManyAsync(IReadOnlyCollection<T> entities)
        {
            return await _commandRepository.AddManyAsync(entities);
        }
        
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.ExistsAsync(expression);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.FindAsync(expression);
        }

        public async Task<T?> FindAsync(string id)
        {
            return await _queryRepository.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(List<Guid> includes)
        {
            return await _queryRepository.ListAsync(includes);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _queryRepository.ListAsync();
        }

        public async Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _queryRepository.ListAsync(expression);
        }

        public async Task<T> PatchAsync(Guid id, T entity)
        {
            return await _commandRepository.PatchAsync(id, entity);
        }

        public async Task<T> PatchAsync(T entity)
        {
            return await _commandRepository.PatchAsync(entity);
        }
    }
}
