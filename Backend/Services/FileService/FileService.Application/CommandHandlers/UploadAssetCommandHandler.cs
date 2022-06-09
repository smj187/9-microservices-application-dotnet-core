using BuildingBlocks.Domain.EfCore;
using FileService.Application.Commands;
using FileService.Application.Services;
using FileService.Core.Domain;
using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, AssetFile>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudService _cloudService;

        public UploadImageCommandHandler(IAssetRepository assetRepository, IUnitOfWork unitOfWork, ICloudService cloudService)
        {
            _assetRepository = assetRepository;
            _unitOfWork = unitOfWork;
            _cloudService = cloudService;
        }

        public async Task<AssetFile> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var urls = await _cloudService.UploadImageAsync(request.Folder, request.Image, request.Title, request.Description, request.Tags);

            var asset = new ImageAsset(request.ExternalEntityId, urls.ToList(), request.AssetType, request.Title, request.Description, request.Tags);

            await _assetRepository.AddAsync(asset);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return asset;
        }
    }
}
