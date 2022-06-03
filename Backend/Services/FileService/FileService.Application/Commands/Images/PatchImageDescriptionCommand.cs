using FileService.Core.Domain.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands.Images
{
    public class PatchImageDescriptionCommand : IRequest<ImageFile>
    {
        public Guid ImageId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
    }
}
