using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles.Actions
{
    public class AddPaginatedCategoriesMappingAction : IMappingAction<PaginatedCategoryResponseDTO, PaginatedCategoryResponse>
    {
        public void Process(PaginatedCategoryResponseDTO source, PaginatedCategoryResponse destination, ResolutionContext context)
        {
            destination.Categories = context.Mapper.Map<IReadOnlyCollection<CategoryResponse>>(source.Categories);
            destination.Pagination = context.Mapper.Map<PaginationResponse>(source.Pagination);
        }
    }
}
