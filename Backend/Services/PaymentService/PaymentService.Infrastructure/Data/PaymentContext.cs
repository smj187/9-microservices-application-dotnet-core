using BuildingBlocks.Multitenancy.Interfaces;
using BuildingBlocks.Multitenancy.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentService.Core.Domain.Aggregates;
using PaymentService.Infrastructure.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Data
{
    public class PaymentContext : DbContext
    {
        private readonly string _tenantId;
        private readonly string _connectionString;

        public PaymentContext(DbContextOptions<PaymentContext> opts, IConfiguration configuration, IMultitenancyService multitenancyService) 
            : base(opts)
        {
            _tenantId = multitenancyService.GetTenantId();
            _connectionString = multitenancyService.GetConnectionString() ?? configuration.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>().HasQueryFilter(a => a.TenantId == _tenantId);

            modelBuilder.ApplyConfiguration(new PaymentEntityTypeConfiguration());

            modelBuilder.UseSerialColumns();
        }

        public DbSet<Payment> Payments { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

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
