using MediaService.Application.Commands;
using MediaService.Application.Repositories;
using MediaService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.CommandHandlers
{
    public class CreateMediaFileCommandHandler : IRequestHandler<CreateMediaFileCommand, MediaFile>
    {
        private readonly IMediaFileRepository _mediaFileRepository;

        public CreateMediaFileCommandHandler(IMediaFileRepository mediaFileRepository)
        {
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<MediaFile> Handle(CreateMediaFileCommand request, CancellationToken cancellationToken)
        {
            return await _mediaFileRepository.CreateMediaFileAsync(request.NewMediaFile);
        }
    }
}
