using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Products;
using CatalogService.Core.Entities.Aggregates;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Products
{
    public class FindProductQueryHandler : IRequestHandler<FindProductQuery, Product>
    {
        private readonly IProductRepository _productRepository;

        public FindProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(FindProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.FindAsync(request.ProductId);
        }
    }
}
