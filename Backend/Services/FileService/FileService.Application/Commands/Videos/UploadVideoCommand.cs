using FileService.Core.Domain.Video;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands.Videos
{
    public class UploadVideoCommand : IRequest<VideoFile>
    {
        public VideoFile NewVideoFile { get; set; }

        public IFormFile File { get; set; }
        public string FolderName { get; set; }
    }
}
