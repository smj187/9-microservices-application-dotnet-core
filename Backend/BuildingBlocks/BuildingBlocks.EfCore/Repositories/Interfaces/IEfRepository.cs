using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Repositories.Interfaces
{
    public interface IEfRepository<T> : IEfCommandRepository<T>, IEfQueryRepository<T> where T : IAggregateBase
    {

    }
}
