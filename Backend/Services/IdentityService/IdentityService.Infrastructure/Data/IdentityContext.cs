using IdentityService.Core.Domain.Admin;
using IdentityService.Core.Domain.User;
using Jwks.Manager;
using Jwks.Manager.Store.EntityFrameworkCore;
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
        public IdentityContext(DbContextOptions<IdentityContext> opts) 
            : base(opts)
        {

        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    //builder.UseSerialColumns();
        //    //base.OnModelCreating(builder);

        //    //builder.Entity<User>().OwnsMany(u => u.RefreshTokens).ToTable("AspNetUserRefreshTokens");
        //}


        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }


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
