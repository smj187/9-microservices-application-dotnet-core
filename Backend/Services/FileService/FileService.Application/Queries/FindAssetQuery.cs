using FileService.Core.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Queries
{
    public class FindAssetQuery : IRequest<AssetFile>
    {
        public Guid AssetId { get; set; }
    }
}
