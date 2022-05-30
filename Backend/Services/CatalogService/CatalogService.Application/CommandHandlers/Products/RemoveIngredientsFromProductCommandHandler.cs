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
    public class RemoveIngredientsFromProductCommandHandler : IRequestHandler<RemoveIngredientsFromProductCommand, Product>
    {
        private readonly IMongoRepository<Product> _mongoRepository;

        public RemoveIngredientsFromProductCommandHandler(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Product> Handle(RemoveIngredientsFromProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _mongoRepository.FindAsync(x => x.Id == request.ProductId);

            if (product == null)
            {
                throw new NotImplementedException();
            }

            product.RemoveIngredients(request.Ingredients);
            product.RemoveAllergen(request.Allergens);
            product.RemoveNutrition(request.Nutritions);



            return await _mongoRepository.PatchAsync(request.ProductId, product);
        }
    }
}
