using FileService.Core.Domain;
using FileService.Core.Domain.Aggregates.Avatar;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands
{
    public class UploadAvatarCommand : IRequest<AvatarAsset>
    {
        public Guid UserId { get; set; }

        public IFormFile Image { get; set; } = default!;
        public AssetType AssetType { get; set; } = default!;

        public string Folder { get; set; } = default!;
    }
}
