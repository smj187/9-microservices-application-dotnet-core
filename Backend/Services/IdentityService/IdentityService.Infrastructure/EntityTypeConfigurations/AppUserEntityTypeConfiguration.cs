using IdentityService.Core.Aggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Infrastructure.EntityTypeConfigurations
{
    public class AppUserEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.InternalIdentityUser).WithOne(x => x.AppUser).HasForeignKey<ApplicationUser>(x => x.InternalUserId);
        }
    }
}
