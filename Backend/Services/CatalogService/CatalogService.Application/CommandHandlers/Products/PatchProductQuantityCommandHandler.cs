﻿using BuildingBlocks.Exceptions.Domain;
using CatalogService.Application.Commands.Products;
using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Products
{
    public class PatchProductQuantityCommandHandler : IRequestHandler<PatchProductQuantityCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public PatchProductQuantityCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(PatchProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.FindAsync(request.ProductId);

            if (product == null)
            {
                throw new AggregateNotFoundException(nameof(Product), request.ProductId);
            }

            product.ChangeQuantity(request.Quantity);

            return await _productRepository.PatchAsync(request.ProductId, product);
        }
    }
}
