using IdentityService.Core.Entities;
using IdentityService.Infrastructure.EntityTypeConfigurations;
using Jwks.Manager;
using Jwks.Manager.Store.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>, ISecurityKeyContext
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }


        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; } = default!;

        //public DbSet<ApplicationUser> Users { get; set; }


        //public override int SaveChanges()
        //{
        //    throw new NotImplementedException();
        //}

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
