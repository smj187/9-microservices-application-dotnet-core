using BuildingBlocks.Multitenancy.Interfaces;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly string _tenantId;
        private readonly string _connectionString;

        public TenantContext(DbContextOptions<TenantContext> opts, IConfiguration configuration, IMultitenancyService multitenancyService) 
            : base(opts)
        {
            _tenantId = multitenancyService.GetTenantId();
            _connectionString = multitenancyService.GetConnectionString() ?? configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>().HasQueryFilter(a => a.TenantId == _tenantId);
            modelBuilder.Entity<Order>().HasQueryFilter(a => a.TenantId == _tenantId);

            modelBuilder.ApplyConfiguration(new TenantEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(_connectionString));
        }

        public DbSet<Tenant> Tenants { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IMultitenantAggregate>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = _tenantId;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
