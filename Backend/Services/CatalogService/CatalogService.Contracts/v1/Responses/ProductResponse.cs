using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Responses
{
    public record ProductResponse(Guid Id, string Name, decimal Price, List<ProductIngredientsResponse> Ingredients, List<ProductAllergensResponse> Allergens, List<ProductNutritionsResponse> Nutritions, bool IsVisible, string Description, string PriceDescription, List<ProductImagesResponse>? Images, List<string>? Tags);
    public record ProductImagesResponse(string Url, string Name, string Description, string Tags);
    public record ProductIngredientsResponse(string Name);
    public record ProductAllergensResponse(string Abbr, string Name);
    public record ProductNutritionsResponse(string Name, int Weight);
}
