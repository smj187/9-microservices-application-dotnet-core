using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Exceptions;
using FileService.Application.Commands.Images;
using FileService.Application.Services;
using FileService.Core.Domain;
using FileService.Core.Domain.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileService.Application.CommandHandlers.Images
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ImageFile>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudService _cloudService;

        public UploadImageCommandHandler(IImageRepository imageRepository, IUnitOfWork unitOfWork, ICloudService cloudService)
        {
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
            _cloudService = cloudService;
        }

        public async Task<ImageFile> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var folder = request.FolderName;
            var title = request.NewImageFile.Title ?? request.File.FileName;
            var description = request.NewImageFile.Description ?? null;
            var tags = request.NewImageFile.Tags ?? null;

            var response = await _cloudService.UploadImageAsync(folder, request.File, title, description, tags);

            request.NewImageFile.AddImages(response.ToList());


            var image = await _imageRepository.AddAsync(request.NewImageFile);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return image;
        }
    }
}
