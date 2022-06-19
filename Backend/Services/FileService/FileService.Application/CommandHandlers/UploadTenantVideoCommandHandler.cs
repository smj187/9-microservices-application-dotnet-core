using BuildingBlocks.EfCore.Repositories.Interfaces;
using FileService.Application.Commands;
using FileService.Application.Services;
using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Tenant;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers
{
    public class UploadTenantVideoCommandHandler : IRequestHandler<UploadTenantVideoCommand, TenantVideoAsset>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudService _cloudService;
        private readonly IAssetRepository _assetRepository;

        public UploadTenantVideoCommandHandler(IUnitOfWork unitOfWork, ICloudService cloudService, IAssetRepository assetRepository)
        {
            _unitOfWork = unitOfWork;
            _cloudService = cloudService;
            _assetRepository = assetRepository;
        }

        public async Task<TenantVideoAsset> Handle(UploadTenantVideoCommand request, CancellationToken cancellationToken)
        {
            var url = await _cloudService.UploadVideoAsync(request.Folder, request.Video, null, null, null);

            var asset = new TenantVideoAsset(request.TenantId, url.Url, request.AssetType);

            await _assetRepository.AddAsync(asset);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return asset;
        }
    }
}
