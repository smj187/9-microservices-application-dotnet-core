using MediaService.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Commands
{
    public class UploadMediaCommand : IRequest<Object>
    {
        public IFormFile File { get; set; }

        public string FolderName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
    }
}
