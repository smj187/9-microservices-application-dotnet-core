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
    public class TenantVideoEntityTypeConfiguration : IEntityTypeConfiguration<TenantVideoAsset>
    {
        public void Configure(EntityTypeBuilder<TenantVideoAsset> builder)
        {
            builder.HasBaseType<AssetFile>();

            builder.Property(x => x.Url).HasColumnName("tenant_video_url");
        }
    }
}
