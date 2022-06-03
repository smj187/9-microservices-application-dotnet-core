using BuildingBlocks.Exceptions;
using FileService.Application.Queries.Images;
using FileService.Core.Domain.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.QueryHandlers.Images
{
    public class FindImageQueryHandler : IRequestHandler<FindImageQuery, ImageFile>
    {
        private readonly IImageRepository _imageRepository;

        public FindImageQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<ImageFile> Handle(FindImageQuery request, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.FindAsync(request.ImageId);
            if (image == null)
            {
                throw new AggregateNotFoundException(nameof(ImageFile), request.ImageId);
            }
            return image;
        }
    }
}
