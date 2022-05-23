using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;
using TenantService.Infrastructure.Data;

namespace TenantService.Application.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly TenantContext _tenantContext;

        public TenantRepository(TenantContext tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public async Task<Tenant> CreateTenantAsync(Tenant tenant)
        {
            _tenantContext.Add(tenant);
            await _tenantContext.SaveChangesAsync();
            return tenant;
        }

        public async Task<IEnumerable<Tenant>> ListTenantsAsync()
        {
            return await _tenantContext.Tenants.ToListAsync();
        }
    }
}
