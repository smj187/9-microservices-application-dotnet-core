using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Image;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class ImageAssetEntityTypeConfiguration : IEntityTypeConfiguration<ImageAsset>
    {
        public void Configure(EntityTypeBuilder<ImageAsset> builder)
        {
            builder.HasBaseType<AssetFile>();

            builder.Property(x => x.Type).HasColumnName("type");
            builder.Property(x => x.Title).HasColumnName("title");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Tags).HasColumnName("tags");

            builder.OwnsMany(x => x.Images, y =>
            {
                y.ToTable("asset_image_urls");
                y.Property("ImageAssetId").HasColumnName("image_file_id");
                y.Property("Id").HasColumnName("id");
                y.Property(z => z.Breakpoint).HasColumnName("breakpoint");
                y.Property(z => z.Url).HasColumnName("image_url");
                y.Property(z => z.Format).HasColumnName("image_format");
                y.Property(z => z.Size).HasColumnName("size");
                y.Property(z => z.Width).HasColumnName("width");
                y.Property(z => z.Height).HasColumnName("height");
            });


        }
    }
}
