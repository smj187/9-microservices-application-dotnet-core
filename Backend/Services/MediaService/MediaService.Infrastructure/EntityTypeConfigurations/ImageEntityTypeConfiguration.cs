using MediaService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Infrastructure.EntityTypeConfigurations
{
    public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<ImageBlob>
    {
        public ImageEntityTypeConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<ImageBlob> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsMany<ImageUrl>(x => x.ImageUrls);
        }
    }
}
