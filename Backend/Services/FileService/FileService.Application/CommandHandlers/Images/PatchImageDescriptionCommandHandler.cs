using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Application.Commands.Images;
using FileService.Core.Domain.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers.Images
{
    public class PatchImageDescriptionCommandHandler : IRequestHandler<PatchImageDescriptionCommand, ImageFile>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PatchImageDescriptionCommandHandler(IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ImageFile> Handle(PatchImageDescriptionCommand request, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.FindAsync(request.ImageId);
            if (image == null)
            {
                throw new AggregateNotFoundException(nameof(ImageFile), request.ImageId);
            }

            image.ChangeDescription(request.Title, request.Description, request.Tags);

            var updated = await _imageRepository.PatchAsync(request.ImageId, image);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return updated;
        }
    }
}
