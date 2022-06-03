using BuildingBlocks.Domain.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Video
{
    public interface IVideoRepository : IEfRepository<VideoFile>
    {

    }
}
