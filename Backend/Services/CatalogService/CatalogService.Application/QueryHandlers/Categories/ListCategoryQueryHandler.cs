using CatalogService.Application.DTOs;
using CatalogService.Application.Queries.Categories;
using CatalogService.Core.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.QueryHandlers.Categories
{
    public class ListCategoryQueryHandler : IRequestHandler<ListCategoryQuery, PaginatedCategoryResponseDTO>
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<PaginatedCategoryResponseDTO> Handle(ListCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.ListAsync(request.Page, request.PageSize);
            return new PaginatedCategoryResponseDTO(result.Item2.ToList(), result.mongoPaginationResult);
        }
    }
}
