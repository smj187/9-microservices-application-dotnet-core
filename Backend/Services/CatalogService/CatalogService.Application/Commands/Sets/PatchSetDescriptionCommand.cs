using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Sets
{
    public class PatchSetDescriptionCommand : IRequest<Set>
    {
        public Guid SetId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public List<string>? Tags { get; set; }
    }
}
