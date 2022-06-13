using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Aggregates;

namespace TenantService.Infrastructure.EntityTypeConfigurations
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.ModifiedAt).HasColumnName("modified_at");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.OwnsOne(x => x.Address, y =>
            {
                y.Property(z => z.Street).HasColumnName("address_street");
                y.Property(z => z.City).HasColumnName("address_city");
                y.Property(z => z.State).HasColumnName("address_state");
                y.Property(z => z.Country).HasColumnName("address_country");
                y.Property(z => z.Zip).HasColumnName("address_zip");
            });


            builder.OwnsMany(x => x.Workingdays, y =>
            {
                y.Property(z => z.Opening).HasColumnName("opening");
                y.Property(z => z.Closing).HasColumnName("closing");
                y.OwnsOne(z => z.Weekday);
            });
        }
    }
}
