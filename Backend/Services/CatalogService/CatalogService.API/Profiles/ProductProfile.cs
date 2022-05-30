using AutoMapper;
using CatalogService.Contracts.v1.Requests.Products;
using CatalogService.Contracts.v1.Responses;
using CatalogService.Core.Entities;
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

            CreateMap<CreateProductIngredientsRequest, Ingredient>()
                .ConstructUsing(src => new Ingredient(src.Name));
            CreateMap<AddIngredientsIngredientsRequest, Ingredient>()
                .ConstructUsing(src => new Ingredient(src.Name));
            CreateMap<RemoveIngredientsIngredientsRequest, Ingredient>()
                .ConstructUsing(src => new Ingredient(src.Name));

            CreateMap<CreateProductAllergensRequest, Allergen>()
                .ConstructUsing(src => new Allergen(src.Abbr, src.Name));
            CreateMap<AddIngredientsAllergensRequest, Allergen>()
                .ConstructUsing(src => new Allergen(src.Abbr, src.Name));
            CreateMap<RemoveIngredientsAllergensRequest, Allergen>()
                .ConstructUsing(src => new Allergen(src.Abbr, src.Name));

            CreateMap<CreateProductNutritionsRequest, Nutrition>()
                .ConstructUsing(src => new Nutrition(src.Name, src.Weight));
            CreateMap<AddIngredientsNutritionsRequest, Nutrition>()
                .ConstructUsing(src => new Nutrition(src.Name, src.Weight));
            CreateMap<RemoveIngredientsNutritionsRequest, Nutrition>()
                .ConstructUsing(src => new Nutrition(src.Name, src.Weight));

            

            CreateMap<CreateProductRequest, Product>()
                .ConstructUsing((src, ctx) =>
                {
                    var ingredients = ctx.Mapper.Map<IEnumerable<Ingredient>>(src.Ingredients);
                    var allergens = ctx.Mapper.Map<IEnumerable<Allergen>>(src.Allergens);
                    var nutritions = ctx.Mapper.Map<IEnumerable<Nutrition>>(src.Nutritions);
                    var tags = ctx.Mapper.Map<IEnumerable<string>>(src.Tags ?? new List<string>());

                    var r = new Product(src.Name, src.Price, ingredients, allergens, nutritions, tags, src.Description, src.PriceDescription);
                    return r;
                });


            

            CreateMap<Product, ProductResponse>();
            CreateMap<Allergen, ProductAllergensResponse>();
            CreateMap<Nutrition, ProductNutritionsResponse>();
            CreateMap<Ingredient, ProductIngredientsResponse>();

        }
    }
}
