using MediaService.Application.Queries;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.QueryHandlers
{
    public class ListMediasQueryHandler : IRequestHandler<ListMediasQuery, IReadOnlyCollection<BaseBlob>>
    {
        private readonly IImageRepository<ImageBlob> _blobRepository;

        public ListMediasQueryHandler(IImageRepository<ImageBlob> blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public async Task<IReadOnlyCollection<BaseBlob>> Handle(ListMediasQuery request, CancellationToken cancellationToken)
        {
            return await _blobRepository.ListAsync();
        }
    }
}
