using FileService.Core.Domain.Image;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands.Images
{
    public class UploadImageCommand : IRequest<ImageFile>
    {
        public ImageFile NewImageFile { get; set; }

        public IFormFile File { get; set; }
        public string FolderName { get; set; }
    }
}
