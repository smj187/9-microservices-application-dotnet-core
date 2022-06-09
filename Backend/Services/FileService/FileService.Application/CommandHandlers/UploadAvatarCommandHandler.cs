using BuildingBlocks.Domain.EfCore;
using FileService.Application.Commands;
using FileService.Application.Services;
using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Avatar;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers
{
    public class UploadAvatarCommandHandler : IRequestHandler<UploadAvatarCommand, AvatarAsset>
    {
        private readonly ICloudService _cloudService;
        private readonly IAssetRepository _assetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UploadAvatarCommandHandler(ICloudService cloudService, IAssetRepository assetRepository, IUnitOfWork unitOfWork)
        {
            _cloudService = cloudService;
            _assetRepository = assetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AvatarAsset> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
        {
            var url = await _cloudService.UploadUserAvatarAsync(request.Folder, request.Image, request.UserId);

            var asset = new AvatarAsset(request.UserId, url, request.AssetType);

            await _assetRepository.AddAsync(asset);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return asset;
        }
    }
}
