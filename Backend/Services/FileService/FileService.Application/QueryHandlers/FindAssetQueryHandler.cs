using BuildingBlocks.Exceptions.Domain;
using FileService.Application.Queries;
using FileService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.QueryHandlers
{
    public class FindAssetQueryHandler : IRequestHandler<FindAssetQuery, AssetFile>
    {
        private readonly IAssetRepository _assetRepository;

        public FindAssetQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<AssetFile> Handle(FindAssetQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetRepository.FindAsync(request.AssetId);
            if (asset == null)
            {
                throw new AggregateNotFoundException(nameof(AssetFile), request.AssetId);
            }

            return asset;
        }
    }
}
