using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public class Repository<T> : IBaseRepository<T>, IRepository<T> where T : IAggregateRoot
    {
        private readonly ICommandRepository<T> _commandRepository;
        private readonly IQueryRepository<T> _queryRepository;

        public Repository(ICommandRepository<T> commandRepository, IQueryRepository<T> queryRepository)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
        }

        public async Task AddAsync(T entity)
        {
            await _commandRepository.AddAsync(entity);
        }

        public async Task<IReadOnlyCollection<T>> ListAsync()
        {
            return await _queryRepository.ListAsync();
        }

    }
}
