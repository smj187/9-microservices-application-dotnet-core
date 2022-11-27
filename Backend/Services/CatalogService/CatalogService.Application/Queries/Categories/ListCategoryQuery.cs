using CatalogService.Application.DTOs;
using CatalogService.Core.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Queries.Categories
{
    public class ListCategoryQuery : IRequest<PaginatedCategoryResponseDTO>
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
