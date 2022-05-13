using CatalogService.Core.Models;
using CatalogService.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ICatalogContext _context;

        public CreateProductCommandHandler(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await _context.Products.InsertOneAsync(request.NewProduct, null, cancellationToken);
            return request.NewProduct;
        }
    }
}
