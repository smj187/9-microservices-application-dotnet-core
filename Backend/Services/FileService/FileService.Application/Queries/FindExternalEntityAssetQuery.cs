using FileService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Queries
{
    public class FindExternalEntityAssetQuery : IRequest<AssetFile>
    {
        public Guid ExternalEntityId { get; set; }
    }
}
