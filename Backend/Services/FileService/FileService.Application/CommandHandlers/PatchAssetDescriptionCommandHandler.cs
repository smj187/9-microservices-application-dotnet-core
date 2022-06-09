using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Application.Commands;
using FileService.Core.Domain.Aggregates;
using MediatR;

namespace FileService.Application.CommandHandlers
{
    public class PatchAssetDescriptionCommandHandler : IRequestHandler<PatchAssetDescriptionCommand, AssetFile>
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchAssetDescriptionCommandHandler(IAssetRepository assetRepository, IUnitOfWork unitOfWork)
        {
            _assetRepository = assetRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssetFile> Handle(PatchAssetDescriptionCommand request, CancellationToken cancellationToken)
        {
            var asset = await _assetRepository.FindAsync(request.AssetId);
            if (asset == null)
            {
                throw new AggregateNotFoundException(nameof(AssetFile), request.AssetId);
            }

            asset.PatchDescription(request.Title, request.Description, request.Tags);

            var patched = await _assetRepository.PatchAsync(request.AssetId, asset);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return patched;
        }
    }
}
