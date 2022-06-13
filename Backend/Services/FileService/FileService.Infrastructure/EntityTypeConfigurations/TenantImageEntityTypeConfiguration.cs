using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class TenantImageEntityTypeConfiguration : IEntityTypeConfiguration<TenantImageAsset>
    {
        public void Configure(EntityTypeBuilder<TenantImageAsset> builder)
        {
            builder.HasBaseType<AssetFile>();

            builder.Property(x => x.Url).HasColumnName("tenant_image_url");
        }
    }
}
