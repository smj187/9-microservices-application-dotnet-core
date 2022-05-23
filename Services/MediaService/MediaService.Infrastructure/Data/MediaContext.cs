using MediaService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Infrastructure.Data
{
    public class MediaContext : DbContext
    {
        public MediaContext(DbContextOptions<MediaContext> opts) : base(opts)
        {

        }

        public DbSet<MediaFile> Files { get; set; } = default!;
    }
}
