using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Video;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class VideoAssetEntityTypeConfiguration : IEntityTypeConfiguration<VideoAsset>
    {
        public void Configure(EntityTypeBuilder<VideoAsset> builder)
        {
            builder.HasBaseType<AssetFile>();

            builder.Property(x => x.Title).HasColumnName("title");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.Tags).HasColumnName("tags");

            builder.OwnsOne(x => x.Video, y =>
            {
                y.Property(z => z.Url).HasColumnName("video_url");
                y.Property(z => z.Format).HasColumnName("video_format");
                y.Property(z => z.Duration).HasColumnName("video_duration");
                y.Property(z => z.Size).HasColumnName("video_size");
                y.Property(z => z.Width).HasColumnName("video_width");
                y.Property(z => z.Height).HasColumnName("video_height");
            });
        }
    }
}
