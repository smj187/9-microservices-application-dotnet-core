using BuildingBlocks.Mongo.Helpers;
using CatalogService.Application.DTOs;
using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Products
{
    public class ListProductsQuery : IRequest<PaginatedProductResponseDTO>
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
