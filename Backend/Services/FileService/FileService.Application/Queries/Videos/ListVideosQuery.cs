using FileService.Core.Domain.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Queries.Videos
{
    public class ListVideosQuery : IRequest<IReadOnlyCollection<VideoFile>>
    {

    }
}
