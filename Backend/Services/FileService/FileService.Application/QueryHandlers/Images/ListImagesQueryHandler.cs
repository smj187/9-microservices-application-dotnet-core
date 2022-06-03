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
    public class ListImagesQueryHandler : IRequestHandler<ListImagesQuery, IReadOnlyCollection<ImageFile>>
    {
        private readonly IImageRepository _imageRepository;

        public ListImagesQueryHandler(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<IReadOnlyCollection<ImageFile>> Handle(ListImagesQuery request, CancellationToken cancellationToken)
        {
            return await _imageRepository.ListAsync();
        }
    }
}
