using BuildingBlocks.EfCore.Interfaces;
using MediaService.Application.Commands;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.CommandHandlers
{
    public class CreateMediaFileCommandHandler : IRequestHandler<CreateMediaFileCommand, Blob>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobRepository<Blob> _blobRepository;

        public CreateMediaFileCommandHandler(IUnitOfWork unitOfWork, IBlobRepository<Blob> blobRepository)
        {
            _unitOfWork = unitOfWork;
            _blobRepository = blobRepository;
        }

        public async Task<Blob> Handle(CreateMediaFileCommand request, CancellationToken cancellationToken)
        {
            await _blobRepository.AddAsync(request.NewMediaFile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return request.NewMediaFile;
        }
    }
}
