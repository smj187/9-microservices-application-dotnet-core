using FileService.Core.Domain.Aggregates;
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
            modelBuilder.ApplyConfiguration(new AssetFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ImageAssetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VideoAssetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarEntityTypeConfiguration());
        }

        public DbSet<AssetFile> AssetFiles { get; set; } = default!;
    }
}
