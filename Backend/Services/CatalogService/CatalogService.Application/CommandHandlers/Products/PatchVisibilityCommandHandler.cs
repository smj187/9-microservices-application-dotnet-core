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
    public class PatchVisibilityCommandHandler : IRequestHandler<PatchVisibilityCommand, Product>
    {
        private readonly IMongoRepository<Product> _mongoRepository;

        public PatchVisibilityCommandHandler(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Product> Handle(PatchVisibilityCommand request, CancellationToken cancellationToken)
        {
            var product = await _mongoRepository.FindAsync(x => x.Id == request.ProductId);
            if (product == null)
            {
                throw new NotImplementedException();
            }

            product.ChangeVisibility(request.IsVisible);

            return await _mongoRepository.PatchAsync(request.ProductId, product);
        }
    }
}
