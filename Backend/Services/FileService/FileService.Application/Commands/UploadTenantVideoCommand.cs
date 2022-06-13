using FileService.Core.Domain;
using FileService.Core.Domain.Aggregates.Tenant;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands
{
    public class UploadTenantVideoCommand : IRequest<TenantVideoAsset>
    {
        public Guid TenantId { get; set; }

        public IFormFile Video { get; set; } = default!;
        public AssetType AssetType { get; set; } = default!;

        public string Folder { get; set; } = default!;
    }
}
