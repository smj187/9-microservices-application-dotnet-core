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
            builder.ToTable("tenant");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasColumnName("id");
            builder.Property(a => a.Name).HasColumnName("name");

            builder.Property(a => a.MinimunOrderAmount).HasColumnName("minimum_order_amount");
            builder.Property(a => a.IsFreeDelivery).HasColumnName("is_free_delivery");
            builder.Property(a => a.DeliveryCost).HasColumnName("delivery_cost");
            builder.Property(a => a.WebsiteUrl).HasColumnName("website_url");
            builder.Property(a => a.Imprint).HasColumnName("website_impres");
            builder.Property(a => a.Email).HasColumnName("email");
            builder.Property(a => a.Phone).HasColumnName("phone");
            builder.Property(a => a.Payments).HasColumnName("payments");
            builder.Property(a => a.Description).HasColumnName("description");

            builder.Property(a => a.CreatedAt).HasColumnName("created_at");
            builder.Property(a => a.ModifiedAt).HasColumnName("modified_at");
            builder.Property(a => a.IsDeleted).HasColumnName("is_deleted");

            builder.OwnsOne(a => a.Address, b =>
            {
                b.Property(c => c.Street).HasColumnName("address_street");
                b.Property(c => c.City).HasColumnName("address_city");
                b.Property(c => c.State).HasColumnName("address_state");
                b.Property(c => c.Country).HasColumnName("address_country");
                b.Property(c => c.Zip).HasColumnName("address_zip");
            });

            builder.OwnsMany(a => a.Workingdays, b =>
            {
                b.ToTable("tenant_working_days");
                b.Property("TenantId").HasColumnName("tenant_id");
                b.Property("Id").HasColumnName("id");
                b.Property(c => c.Opening).HasColumnName("opening_time");
                b.Property(c => c.Closing).HasColumnName("closing_time");
                b.Property(c => c.Message).HasColumnName("extra_message");
                b.Property(c => c.IsClosedToday).HasColumnName("is_closed_today");
                b.OwnsOne(c => c.Weekday, d =>
                {
                    d.Property(e => e.Value).HasColumnName("weekday_value");
                    d.Property(e => e.Description).HasColumnName("weekday_description");
                });
            });

            builder.Property(a => a.BrandImageAssetId).HasColumnName("asset_brand_image");
            builder.Property(a => a.LogoAssetId).HasColumnName("asset_logo");
            builder.Property(a => a.VideoAssetId).HasColumnName("asset_video");
            builder.Property(a => a.BannerAssetId).HasColumnName("asset_banner");
        }
    }
}
