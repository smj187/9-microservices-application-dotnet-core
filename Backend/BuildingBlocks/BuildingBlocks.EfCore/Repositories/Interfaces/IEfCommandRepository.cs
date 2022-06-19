using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Repositories.Interfaces
{
    public interface IEfCommandRepository<T> : ICommandRepository<T> where T : IAggregateBase
    {

    }
}
