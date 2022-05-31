using CatalogService.Core.Entities.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Groups
{
    public class PatchGroupDescriptionCommand : IRequest<Group>
    {
        public Guid GroupId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public List<string>? Tags { get; set; }
    }
}
