using MediaService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Repositories
{
    public interface IMediaFileRepository
    {
        Task<IEnumerable<MediaFile>> ListMediaFilesAsync();
        Task<MediaFile> CreateMediaFileAsync(MediaFile mediaFile);
    }
}
