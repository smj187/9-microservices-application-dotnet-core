using CatalogService.Application.Queries;
using CatalogService.Application.Repositories.Products;
using CatalogService.Core.Entities;
using CatalogService.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers
{
    public class ListProductQueryHandler : IRequestHandler<ListProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductQueryRepository _productQueryRepository;

        public ListProductQueryHandler(IProductQueryRepository productQueryRepository)
        {
            _productQueryRepository = productQueryRepository;
        }

        public async Task<IEnumerable<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productQueryRepository.ListAsync();
        }
    }
}
