using AutoMapper;
using CatalogService.Contracts.v1;
using CatalogService.Core.Entities;
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
            // requests
            CreateMap<CreateCategoryRequest, Category>()
                .ConstructUsing((src, ctx) =>
                {
                    return new Category(src.Name, src.Description);
                });



            // responses
            CreateMap<Category, CategoryResponse>();

      
        }
    }
}
