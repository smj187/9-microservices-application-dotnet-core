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
    public class FindAvatarQueryHandler : IRequestHandler<FindAvatarQuery, AssetFile>
    {
        private readonly IAssetRepository _assetRepository;

        public FindAvatarQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<AssetFile> Handle(FindAvatarQuery request, CancellationToken cancellationToken)
        {
            var avatar = await _assetRepository.FindAsync(x => x.ExternalEntityId == request.ExternalEntityId);
            if (avatar == null)
            {
                throw new AggregateNotFoundException(nameof(AssetFile), request.ExternalEntityId);
            }

            return avatar;
        }
    }
}
