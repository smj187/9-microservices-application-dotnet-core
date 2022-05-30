using AutoMapper;
using CatalogService.Contracts.v1.Requests.Categories;
using CatalogService.Contracts.v1.Responses;
using CatalogService.Core.Entities;
using CatalogService.Core.Models;
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

            CreateMap<CreateCategoryRequest, Category>()
                .ConstructUsing((src, ctx) =>
                {
                    return new Category(src.Name, src.Description);
                });

            CreateMap<Category, CategorySummaryResponse>();



            CreateMap<CategoryDetailsModel, CategoryDetailsResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Category.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(s => s.Category.Description))
                .ForMember(dest => dest.Images, opts => opts.MapFrom(s => s.Category.Images));
            CreateMap<Product, CategoryProductResponse>();
        }
    }
}
