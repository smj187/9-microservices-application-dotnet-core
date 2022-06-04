using AutoMapper;
using CatalogService.Contracts.v1;
using CatalogService.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CatalogService.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // requests
            CreateMap<IngredientsRequest, Ingredient>()
                .ConstructUsing(src => new Ingredient(src.Name));

            CreateMap<AllergensRequest, Allergen>()
                .ConstructUsing(src => new Allergen(src.Abbr, src.Name));

            CreateMap<NutritionsRequest, Nutrition>()
                .ConstructUsing(src => new Nutrition(src.Name, src.Weight));

            CreateMap<CreateProductRequest, Product>()
                .ConstructUsing((src, ctx) =>
                {
                    var ingredients = src.Ingredients != null ? ctx.Mapper.Map<IEnumerable<Ingredient>>(src.Ingredients) : null;
                    var allergens = src.Allergens != null ? ctx.Mapper.Map<IEnumerable<Allergen>>(src.Allergens) : null;
                    var nutritions = src.Nutritions != null ? ctx.Mapper.Map<IEnumerable<Nutrition>>(src.Nutritions) : null;

                    var tags = ctx.Mapper.Map<IEnumerable<string>>(src.Tags ?? new List<string>());

                    return new Product(src.Name, src.Price, ingredients, allergens, nutritions, tags, src.Description, src.PriceDescription);
                });


            // responses
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Images, opts => opts.MapFrom(s => s.Images));
            CreateMap<Allergen, AllergensResponse>();
            CreateMap<Nutrition, NutritionsResponse>();
            CreateMap<Ingredient, IngredientsResponse>();
        }
    }
}
