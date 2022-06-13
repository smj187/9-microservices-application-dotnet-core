using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;
using TenantService.Infrastructure.EntityTypeConfigurations;

namespace TenantService.Infrastructure.Data
{
    public class TenantContext : DbContext
    {
        public TenantContext(DbContextOptions<TenantContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantEntityTypeConfiguration());
        }

        public DbSet<Tenant> Tenants { get; set; } = default!;
    }
}
