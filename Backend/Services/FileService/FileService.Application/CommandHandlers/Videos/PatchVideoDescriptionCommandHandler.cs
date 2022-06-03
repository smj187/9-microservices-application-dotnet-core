using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Application.Commands.Videos;
using FileService.Core.Domain.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers.Videos
{
    public class PatchVideoDescriptionCommandHandler : IRequestHandler<PatchVideoDescriptionCommand, VideoFile>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchVideoDescriptionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
        {
            _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<VideoFile> Handle(PatchVideoDescriptionCommand request, CancellationToken cancellationToken)
        {
            var video = await _videoRepository.FindAsync(request.VideoId);
            if (video == null)
            {
                throw new AggregateNotFoundException(nameof(VideoFile), request.VideoId);
            }

            video.ChangeDescription(request.Title, request.Description, request.Tags);

            var updated = await _videoRepository.PatchAsync(request.VideoId, video);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return updated;
        }
    }
}
