using MediaService.Core.Entities;
using MediaService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Repositories
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly MediaContext _mediaContext;

        public MediaFileRepository(MediaContext mediaContext)
        {
            _mediaContext = mediaContext;
        }

        public async Task<MediaFile> CreateMediaFileAsync(MediaFile mediaFile)
        {
            _mediaContext.Add(mediaFile);
            await _mediaContext.SaveChangesAsync();
            return mediaFile;
        }

        public async Task<IEnumerable<MediaFile>> ListMediaFilesAsync()
        {
            return await _mediaContext.Files.ToListAsync();
        }
    }
}
