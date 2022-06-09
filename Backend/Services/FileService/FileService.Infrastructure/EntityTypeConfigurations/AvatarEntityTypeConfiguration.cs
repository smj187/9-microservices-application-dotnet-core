using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Avatar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class AvatarEntityTypeConfiguration : IEntityTypeConfiguration<AvatarAsset>
    {
        public void Configure(EntityTypeBuilder<AvatarAsset> builder)
        {
            builder.HasBaseType<AssetFile>();

            builder.Property(x => x.Url).HasColumnName("avatar_url");
        }
    }
}
