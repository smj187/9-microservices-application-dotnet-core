using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;

namespace TenantService.Application.Repositories
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> ListTenantsAsync();
        Task<Tenant> CreateTenantAsync(Tenant tenant);
    }
}
