using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;

namespace TenantService.Infrastructure.Data
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> opts) : base(opts)
        {

        }

        public DbSet<Tenant> Tenants { get; set; } = default!;
    }
}
