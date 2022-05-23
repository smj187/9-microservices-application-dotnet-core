using MediaService.Application.Queries;
using MediaService.Application.Repositories;
using MediaService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.QueryHandlers
{
    public class ListMediaFileQueryHandler : IRequestHandler<ListMediaFileQuery, IEnumerable<MediaFile>>
    {
        private readonly IMediaFileRepository _mediaFileRepository;

        public ListMediaFileQueryHandler(IMediaFileRepository mediaFileRepository)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<IEnumerable<MediaFile>> Handle(ListMediaFileQuery request, CancellationToken cancellationToken)
        {
            return await _mediaFileRepository.ListMediaFilesAsync();
        }
    }
}
