using FileService.Application.Queries.Videos;
using FileService.Core.Domain.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.QueryHandlers.Videos
{
    public class ListVideosQueryHandler : IRequestHandler<ListVideosQuery, IReadOnlyCollection<VideoFile>>
    {
        private readonly IVideoRepository _videoRepository;

        public ListVideosQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<IReadOnlyCollection<VideoFile>> Handle(ListVideosQuery request, CancellationToken cancellationToken)
        {
            return await _videoRepository.ListAsync();
        }
    }
}
