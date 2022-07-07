using BuildingBlocks.Multitenancy.Interfaces.Services;
using FileService.Core.Domain.Aggregates;
using FileService.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Data
{
    public class FileContext : DbContext
    {
        private readonly string _tenantId;
        private readonly string _connectionString;

        public FileContext(DbContextOptions<FileContext> opts, IConfiguration config, IMultitenancyService multitenancyService)
            : base(opts)
        {
            _tenantId = multitenancyService.GetTenantId();
            _connectionString = multitenancyService.GetConnectionString() ?? config.GetConnectionString("DefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AssetFile>().HasQueryFilter(a => a.TenantId == _tenantId);

            modelBuilder.ApplyConfiguration(new AssetFileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ImageAssetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VideoAssetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AvatarEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TenantImageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TenantVideoEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(_connectionString));
        }

        public DbSet<AssetFile> AssetFiles { get; set; } = default!;
    }
}
