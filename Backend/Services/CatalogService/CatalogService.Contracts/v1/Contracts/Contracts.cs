using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Contracts
{
    // create new category
    public record CreateCategoryRequest([Required] string Name, string? Description, List<Guid>? Products, List<Guid>? Sets);

    // patch category description
    public record PatchCategoryDescriptionRequest([Required] string Name, string? Description);


    // create new set
    public record CreateSetRequest([Required] string Name, [Required] decimal Price, string? Description, string? PriceDescription, List<string>? Tags, int? Quantity);

    // patch set description
    public record PatchSetDescriptionRequest([Required] string Name, string? Description, string? PriceDescription, List<string>? Tags);

    // patch set price
    public record PatchSetPriceRequest([Required] decimal Price);

    // patch set visibility
    public record PatchSetVisibilityRequest([Required] bool IsVisible);

    // patch set availability
    public record PatchSetAvailabilityRequest([Required] bool IsAvailable);

    // patch set quantity
    public record PatchSetQuantityRequest(int? Quantity);


    // create new product
    public record CreateProductRequest([Required] string Name, [Required] decimal Price, List<IngredientsRequest>? Ingredients, List<AllergensRequest>? Allergens, List<NutritionsRequest>? Nutritions, string? Description, string? PriceDescription, List<string>? Tags, int? Quantity);
    public record IngredientsRequest([Required] string Name);
    public record AllergensRequest([Required] string Abbr, [Required] string Name);
    public record NutritionsRequest([Required] string Name, [Required] int Weight);

    // manage product ingredients
    public record AddIngredientsRequest(List<IngredientsRequest> Ingredients, List<AllergensRequest> Allergens, List<NutritionsRequest> Nutritions);
    public record RemoveIngredientsRequest(List<IngredientsRequest> Ingredients, List<AllergensRequest> Allergens, List<NutritionsRequest> Nutritions);

    // patch product description
    public record PatchProductDescriptionRequest([Required] string Name, string? Description, string? PriceDescription, List<string>? Tags);

    // patch product price
    public record PatchProductPriceRequest([Required] decimal Price);

    // patch product visibility
    public record PatchProductVisibilityRequest([Required] bool IsVisible);

    // patch product availability
    public record PatchProductAvailabilityRequest([Required] bool IsAvailable);

    // patch product quantity
    public record PatchProductQuantityRequest(int? Quantity);





    // generel category response
    public record CategoryResponse(Guid Id, string Name, string Description, bool IsVisible, List<Guid> Products, List<Guid> Sets, List<Guid> Assets);


    // general set response
    public record SetResponse(Guid Id, string Name, string Description, string Price, string PriceDescription, bool IsVisible, bool IsAvailable, int? Quantity, List<Guid> Assets, List<Guid> Products, List<string> Tags);


    // general product response
    public record ProductResponse(Guid Id, string Name, decimal Price, int? Quantity, bool IsAvailable, bool IsVisible, string Description, string PriceDescription, List<IngredientsResponse> Ingredients, List<AllergensResponse> Allergens, List<NutritionsResponse> Nutritions, List<Guid> Assets, List<string> Tags);


    // ingredients response
    public record IngredientsResponse(string Name);
    public record AllergensResponse(string Abbr, string Name);
    public record NutritionsResponse(string Name, int Weight);

}
