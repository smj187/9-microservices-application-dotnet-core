using FileService.Core.Domain.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands.Users
{
    public class UploadAvatarCommand : IRequest<Avatar>
    {
        public Guid UserId { get; set; }

        public IFormFile File { get; set; } = default!;
        public string FolderName { get; set; } = default!;
    }
}
