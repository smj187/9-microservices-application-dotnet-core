using AutoMapper;
using CatalogService.API.Profiles.Actions;
using CatalogService.Application.DTOs;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDetailsResponse>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<PaginatedCategoryResponseDTO, PaginatedCategoryResponse>()
                .AfterMap<AddPaginatedCategoriesMappingAction>();
        }
    }
}