using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Products
{
    public class PatchProductVisibilityCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public bool IsVisible { get; set; }
    }
}
