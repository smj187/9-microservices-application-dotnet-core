using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Sets
{
    public class CreateSetCommand : IRequest<Set>
    {
        public string Name { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string? Description { get; set; } = null!;
        public string? PriceDescription { get; set; } = null!;
        public List<string>? Tags { get; set; } = null;
        public int? Quantity { get; set; } = null;
    }
}
