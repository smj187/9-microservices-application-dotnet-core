using CatalogService.Application.Queries.Products;
using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Products
{
    public class ListProductQueryHandler : IRequestHandler<ListProductsQuery, IReadOnlyCollection<Product>>
    {
        private readonly IProductRepository _productRepository;

        public ListProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.ListAsync();
        }
    }
}
