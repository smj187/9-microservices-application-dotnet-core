using FileService.Core.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class AssetFileEntityTypeConfiguration : IEntityTypeConfiguration<AssetFile>
    {
        public void Configure(EntityTypeBuilder<AssetFile> builder)
        {
            builder.ToTable("asset");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ExternalEntityId).HasColumnName("external_entity_id");

            builder.OwnsOne(x => x.AssetType, y =>
            {
                y.Property(z => z.Value).HasColumnName("asset_type_value");
                y.Property(z => z.Description).HasColumnName("asset_type_description");
            });

            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.ModifiedAt).HasColumnName("modified_at");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.ExternalEntityId).HasColumnName("external_entity_id");
            
        }
    }
}
