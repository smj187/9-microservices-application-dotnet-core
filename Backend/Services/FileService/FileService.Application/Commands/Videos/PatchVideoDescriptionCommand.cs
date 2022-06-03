using FileService.Core.Domain.Video;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands.Videos
{
    public class PatchVideoDescriptionCommand : IRequest<VideoFile>
    {
        public Guid VideoId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
    }
}
