using BuildingBlocks.Mongo.Helpers;
using CatalogService.Application.DTOs;
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
    public class ListProductQueryHandler : IRequestHandler<ListProductsQuery, PaginatedProductResponseDTO>
    {
        private readonly IProductRepository _productRepository;

        public ListProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PaginatedProductResponseDTO> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.ListAsync(request.Page, request.PageSize);
            return new PaginatedProductResponseDTO(result.Item2.ToList(), result.mongoPaginationResult);
         
        }
    }
}
