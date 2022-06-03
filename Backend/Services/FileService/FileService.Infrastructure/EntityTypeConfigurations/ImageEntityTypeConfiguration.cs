using FileService.Core.Domain.Image;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.EntityTypeConfigurations
{
    public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<ImageFile>
    {
        public void Configure(EntityTypeBuilder<ImageFile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.ModifiedAt);
            builder.Property(x => x.IsDeleted);
            builder.OwnsMany(x => x.Images);

        }
    }
}
