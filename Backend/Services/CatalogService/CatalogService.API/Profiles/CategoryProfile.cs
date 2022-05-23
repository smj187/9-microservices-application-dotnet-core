using AutoMapper;
using CatalogService.API.Contracts.Reponses;
using CatalogService.API.Contracts.Requests;
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
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<Category, CategoryResponse>();
        }
    }
}
