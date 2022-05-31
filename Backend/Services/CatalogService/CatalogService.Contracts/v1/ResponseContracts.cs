using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1
{
    // generel category response
    public record CategoryResponse(Guid Id, string Name, string Description, bool IsVisible, List<Guid> Products, List<CategoryImagesResponse> Images);

    // images 
    public record CategoryImagesResponse(string Url, string Name, string Description, string Tags);




    // general group response
    public record GroupResponse(Guid Id, string Name, string Description, string Price, string PriceDescription, bool IsVisible, List<GroupImagesResponse> Images, List<Guid> Products, List<string> Tags);

    // image response
    public record GroupImagesResponse(string Url, string Name, string Description, string Tags);




    // general product response
    public record ProductResponse(Guid Id, string Name, decimal Price, List<IngredientsResponse> Ingredients, List<AllergensResponse> Allergens, List<NutritionsResponse> Nutritions, bool IsVisible, string Description, string PriceDescription, List<ProductImagesResponse> Images, List<string> Tags);

    // ingredients response
    public record IngredientsResponse(string Name);
    public record AllergensResponse(string Abbr, string Name);
    public record NutritionsResponse(string Name, int Weight);

    // image response
    public record ProductImagesResponse(string Url, string Name, string Description, string Tags);
}
