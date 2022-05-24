using MediaService.Application.Queries;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Data;
using MediaService.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.QueryHandlers
{
    public class ListMediaFileQueryHandler : IRequestHandler<ListMediaFileQuery, IEnumerable<Blob>>
    {
        private readonly IBlobRepository<Blob> _blobRepository;

        public ListMediaFileQueryHandler(IBlobRepository<Blob> blobRepository)
        {
            _blobRepository = blobRepository;
        }

        public async Task<IEnumerable<Blob>> Handle(ListMediaFileQuery request, CancellationToken cancellationToken)
        {
            return await _blobRepository.ListAsync();
        }
    }
}
