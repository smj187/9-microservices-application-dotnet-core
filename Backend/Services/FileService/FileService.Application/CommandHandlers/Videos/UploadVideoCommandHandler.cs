using BuildingBlocks.Domain.EfCore;
using FileService.Application.Commands.Videos;
using FileService.Application.Services;
using FileService.Core.Domain.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers.Videos
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, VideoFile>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudService _cloudService;

        public UploadVideoCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork, ICloudService cloudService)
        {
            _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
            _cloudService = cloudService;
        }

        public async Task<VideoFile> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var folder = request.FolderName;
            var title = request.NewVideoFile.Title ?? request.File.FileName;
            var description = request.NewVideoFile.Description ?? null;
            var tags = request.NewVideoFile.Tags ?? null;

            var response = await _cloudService.UploadVideoAsync(folder, request.File, title, description, tags);

            request.NewVideoFile.AddVideo(response);

            var video = await _videoRepository.AddAsync(request.NewVideoFile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return video;

        }
    }
}
