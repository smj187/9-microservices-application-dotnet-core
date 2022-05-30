using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Products;
using CatalogService.Core.Entities;
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
        private readonly IMongoRepository<Product> _mongoRepository;

        public PatchProductDescriptionCommandHandler(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Product> Handle(PatchProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            var product = await _mongoRepository.FindAsync(request.ProductId);
            if (product == null)
            {
                throw new NotImplementedException();
            }

            product.ChangeDescription(request.Name, request.Description, request.PriceDescription, request.Tags);

            return await _mongoRepository.PatchAsync(request.ProductId, product);
        }
    }
}
