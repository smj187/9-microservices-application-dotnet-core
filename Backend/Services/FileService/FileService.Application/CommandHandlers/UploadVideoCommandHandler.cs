using BuildingBlocks.EfCore.Repositories.Interfaces;
using FileService.Application.Commands;
using FileService.Application.Services;
using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers
{
    public class UploadVideoCommandHandler : IRequestHandler<UploadVideoCommand, VideoAsset>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudService _cloudService;
        private readonly IAssetRepository _assetRepository;

        public UploadVideoCommandHandler(IUnitOfWork unitOfWork, ICloudService cloudService, IAssetRepository assetRepository)
        {
            _unitOfWork = unitOfWork;
            _cloudService = cloudService;
            _assetRepository = assetRepository;
        }

        public async Task<VideoAsset> Handle(UploadVideoCommand request, CancellationToken cancellationToken)
        {
            var response = await _cloudService.UploadVideoAsync(request.Folder, request.Video, request.Title, request.Description, request.Tags);

            var asset = new VideoAsset(request.ExternalEntityId, response, request.AssetType, request.TenantId, request.Title, request.Description, request.Tags);

            await _assetRepository.AddAsync(asset);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return asset;
        }
    }
}
