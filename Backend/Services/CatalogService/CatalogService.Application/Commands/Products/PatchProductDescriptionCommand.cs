using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Products
{
    public class PatchProductDescriptionCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public List<string>? Tags { get; set; }
    }
}
