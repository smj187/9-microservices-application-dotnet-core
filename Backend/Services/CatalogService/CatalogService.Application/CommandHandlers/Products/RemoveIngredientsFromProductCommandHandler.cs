using BuildingBlocks.Exceptions;
using CatalogService.Application.Commands.Products;
using CatalogService.Core.Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
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


            var product = await _productRepository.FindAsync(request.ProductId);

            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), request.ProductId);
            }

            product.RemoveIngredients(request.Ingredients);
            product.RemoveAllergen(request.Allergens);
            product.RemoveNutrition(request.Nutritions);



            return await _productRepository.PatchAsync(request.ProductId, product);
        }
    }
}
