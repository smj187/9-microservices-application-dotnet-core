using BuildingBlocks.Exceptions;
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
    public class FindVideoQueryHandler : IRequestHandler<FindVideoQuery, VideoFile>
    {
        private readonly IVideoRepository _videoRepository;

        public FindVideoQueryHandler(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<VideoFile> Handle(FindVideoQuery request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.FindAsync(request.VideoId);
            if (video == null)
            {
                throw new AggregateNotFoundException(nameof(VideoFile), request.VideoId);
            }
            return video;
        }
    }
}
