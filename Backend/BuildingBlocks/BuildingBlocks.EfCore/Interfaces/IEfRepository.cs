using BuildingBlocks.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Interfaces
{
    public interface IEfRepository<T> : IEfCommandRepository<T>, IEfQueryRepository<T> where T : IAggregateBase
    {

    }
}
