using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Repositories.Interfaces
{
    public interface IEfQueryRepository<T> : IQueryRepository<T> where T : IAggregateBase
    {

    }
}
