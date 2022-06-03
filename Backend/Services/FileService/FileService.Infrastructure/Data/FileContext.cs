using FileService.Core.Domain.Image;
using FileService.Core.Domain.Video;
using FileService.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Data
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> opts)
            : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VideoEntityTypeConfiguration());
        }

        public DbSet<ImageFile> Images { get; set; } = default!;
        public DbSet<VideoFile> Videos { get; set; } = default!;
    }
}
