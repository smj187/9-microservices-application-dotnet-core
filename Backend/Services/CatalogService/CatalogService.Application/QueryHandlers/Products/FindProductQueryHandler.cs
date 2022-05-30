using BuildingBlocks.Mongo;
using CatalogService.Application.Queries.Products;
using CatalogService.Core.Entities;
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
        private readonly IMongoRepository<Product> _mongoRepository;

        public FindProductQueryHandler(IMongoRepository<Product> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Product> Handle(FindProductQuery request, CancellationToken cancellationToken)
        {
            return await _mongoRepository.FindAsync(request.ProductId);
        }
    }
}
