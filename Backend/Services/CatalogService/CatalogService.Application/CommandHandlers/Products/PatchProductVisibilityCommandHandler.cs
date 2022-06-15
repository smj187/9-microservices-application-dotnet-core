using BuildingBlocks.Exceptions;
using CatalogService.Application.Commands.Products;
using CatalogService.Core.Domain.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Products
{
    public class PatchProductVisibilityCommandHandler : IRequestHandler<PatchProductVisibilityCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public PatchProductVisibilityCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(PatchProductVisibilityCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), request.ProductId);
            }

            product.ChangeVisibility(request.IsVisible);

            return await _productRepository.PatchAsync(request.ProductId, product);
        }
    }
}
