using BuildingBlocks.Multitenancy.Interfaces;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;

namespace IdentityService.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<InternalIdentityUser, InternalRole, Guid>, ISecurityKeyContext
    {
        private readonly string? _tenantId = null;
        private readonly string _connectionString;

        public IdentityContext(DbContextOptions<IdentityContext> opts, IConfiguration config, IMultitenancyService multitenancyService)
            : base(opts)
        {
            _tenantId = multitenancyService.GetTenantId();
            _connectionString = multitenancyService.GetConnectionString() ?? config.GetConnectionString("DefaultConnection");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(a => a.TenantId == _tenantId);

            modelBuilder.ApplyConfiguration(new AppUserEntityTypeConfiguration());
        }


        //public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; } = default!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;
        public DbSet<KeyMaterial> SecurityKeys { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(_connectionString));
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

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{

        //    var modifiedEntites = ChangeTracker.Entries()
        //        .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
        //        .ToList();

        //    foreach (var entry in modifiedEntites)
        //    {
        //        foreach (var prop in entry.Properties)
        //        {
        //            if (prop.Metadata.ClrType == typeof(DateTime))
        //            {
        //                prop.Metadata.FieldInfo.SetValue(entry.Entity, DateTime.SpecifyKind((DateTime)prop.CurrentValue, DateTimeKind.Utc));
        //            }
        //            else if (prop.Metadata.ClrType == typeof(DateTime?) && prop.CurrentValue != null)
        //            {
        //                prop.Metadata.FieldInfo.SetValue(entry.Entity, DateTime.SpecifyKind(((DateTime?)prop.CurrentValue).Value, DateTimeKind.Utc));
        //            }
        //        }
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}


    }
}
