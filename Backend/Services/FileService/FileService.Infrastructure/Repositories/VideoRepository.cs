using BuildingBlocks.Domain.EfCore;
using FileService.Core.Domain.Video;
using FileService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Repositories
{
    public class VideoRepository : EfRepository<VideoFile>, IVideoRepository
    {
        public VideoRepository(FileContext context)
            : base(context)
        {

        }
    }
}
