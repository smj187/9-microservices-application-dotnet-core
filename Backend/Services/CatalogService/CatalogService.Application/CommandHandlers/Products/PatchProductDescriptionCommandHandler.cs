using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Products;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Products
{
    public class PatchProductDescriptionCommandHandler : IRequestHandler<PatchProductDescriptionCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public PatchProductDescriptionCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(PatchProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new NotImplementedException();
            }

            product.ChangeDescription(request.Name, request.Description, request.PriceDescription, request.Tags);

            return await _productRepository.PatchAsync(request.ProductId, product);
        }
    }
}
