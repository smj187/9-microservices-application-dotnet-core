using FileService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Commands
{
    public class PatchAssetDescriptionCommand : IRequest<AssetFile>
    {
        public Guid AssetId { get; set; }

        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
    }
}
