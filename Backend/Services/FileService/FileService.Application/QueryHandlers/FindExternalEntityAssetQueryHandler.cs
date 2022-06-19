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
    public class FindExternalEntityAssetQueryHandler : IRequestHandler<FindExternalEntityAssetQuery, AssetFile>
    {
        private readonly IAssetRepository _assetRepository;

        public FindExternalEntityAssetQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<AssetFile> Handle(FindExternalEntityAssetQuery request, CancellationToken cancellationToken)
        {
            var asset = await _assetRepository.FindAsync(x => x.ExternalEntityId == request.ExternalEntityId);
            if (asset == null)
            {
                throw new AggregateNotFoundException($"no asset has an assigned '{request.ExternalEntityId}' id");
            }

            return asset;
        }
    }
}
