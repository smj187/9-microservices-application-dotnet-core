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
    public class ListAssetsQueryHandler : IRequestHandler<ListAssetsQuery, IReadOnlyCollection<AssetFile>>
    {
        private readonly IAssetRepository _assetRepository;

        public ListAssetsQueryHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<IReadOnlyCollection<AssetFile>> Handle(ListAssetsQuery request, CancellationToken cancellationToken)
        {
            return await _assetRepository.ListAsync(x => x.ExternalEntityId == request.ExternalEntityId);
        }
    }
}
