using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Infrastructure.Repositories
{
    public interface IBlobRepository<T> : IRepository<T> where T : IAggregateRoot
    {

    }
}
