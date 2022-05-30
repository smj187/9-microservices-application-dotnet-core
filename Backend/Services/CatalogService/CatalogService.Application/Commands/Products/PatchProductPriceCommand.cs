using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Products
{
    public class PatchProductPriceCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
