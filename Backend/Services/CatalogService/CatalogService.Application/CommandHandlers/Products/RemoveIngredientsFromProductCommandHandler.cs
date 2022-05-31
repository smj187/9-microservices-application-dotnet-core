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
    public class RemoveIngredientsFromProductCommandHandler : IRequestHandler<RemoveIngredientsFromProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public RemoveIngredientsFromProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(RemoveIngredientsFromProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(x => x.Id == request.ProductId);

            if (product == null)
            {
                throw new NotImplementedException();
            }

            product.RemoveIngredients(request.Ingredients);
            product.RemoveAllergen(request.Allergens);
            product.RemoveNutrition(request.Nutritions);



            return await _productRepository.PatchAsync(request.ProductId, product);
        }
    }
}
