using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Infrastructure.Repositories
{
    public interface ITenantRepository<T> : IRepository<T> where T : IAggregateRoot
    {

    }
}
