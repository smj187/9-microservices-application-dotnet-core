using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1
{
    #region requests

    // create new category
    public class CreateCategoryRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<Guid>? Products { get; set; }
        public List<Guid>? Sets { get; set; }
    }

    // patch category description
    public class PatchCategoryDescriptionRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }

    // patch category availability
    public class PatchCategoryVisibilityRequest
    {
        public required bool IsVisible { get; set; }
    }

    // create new set
    public class CreateSetRequest
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public int? Quantity { get; set; }
        public List<string>? Tags { get; set; }
    }

    // patch set description
    public class PatchSetDescriptionRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public List<string>? Tags { get; set; }
    }

    // patch set price
    public class PatchSetPriceRequest
    {
        public required decimal Price { get; set; }
    }

    // patch set visibility
    public class PatchSetVisibilityRequest
    {
        public required bool IsVisible { get; set; }
    }

    // patch set availability
    public class PatchSetAvailabilityRequest
    {
        public required bool IsAvailable { get; set; }
    }

    // patch set quantity
    public class PatchSetQuantityRequest
    {
        public int? Quantity { get; set; }
    }

    // create new product
    public class CreateProductRequest
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public int? Quantity { get; set; }
        public List<IngredientsRequest>? Ingredients { get; set; }
        public List<AllergensRequest>? Allergens { get; set; }
        public List<NutritionsRequest>? Nutritions { get; set; }
        public List<string>? Tags { get; set; }
    }

    public class IngredientsRequest
    {
        public required string Name { get; set; }
    }
    public class AllergensRequest
    {
        public required string Name { get; set; }
        public required string Abbr { get; set; }
    }
    public class NutritionsRequest
    {
        public required string Name { get; set; }
        public required int Weight { get; set; }
    }

    // manage product ingredients
    public class AddIngredientsRequest
    {
        public required List<IngredientsRequest> Ingredients { get; set; }
        public required List<AllergensRequest> Allergens { get; set; }
        public required List<NutritionsRequest> Nutritions { get; set; }
    }
    public class RemoveIngredientsRequest
    {
        public required List<IngredientsRequest> Ingredients { get; set; }
        public required List<AllergensRequest> Allergens { get; set; }
        public required List<NutritionsRequest> Nutritions { get; set; }
    }

    // patch product description
    public class PatchProductDescriptionRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public List<string>? Tags { get; set; }
    }

    // patch product price
    public class PatchProductPriceRequest
    {
        public required decimal Price { get; set; }
    }

    // patch product visibility
    public class PatchProductVisibilityRequest
    {
        public required bool IsVisible { get; set; }
    }

    // patch product availability
    public class PatchProductAvailabilityRequest
    {
        public required bool IsAvailable { get; set; }
    }

    // patch product quantity
    public class PatchProductQuantityRequest
    {
        public int? Quantity { get; set; }
    }



    #region responses

    // generel category response
    public class CategoryResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required bool IsVisible { get; set; }
        public required List<Guid> Products { get; set; }
        public required List<Guid> Sets { get; set; }
        public required List<Guid> Assets { get; set; }
    }

    public class CategoryDetailsResponse
    {
        public required Guid Id { get; set; }
        public required string TenantId { get; set; }
        public required string Name { get; set; }
        public required bool IsVisible { get; set; }
        public required string? Description { get; set; }
        public required List<Guid> Products { get; set; }
        public required List<Guid> Sets { get; set; }
        public required List<Guid> Assets { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }

    // general set response
    public class SetResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Price { get; set; }
        public required bool IsVisible { get; set; }
        public required bool IsAvailable { get; set; }
        public required List<Guid> Products { get; set; }
        public required List<string> Tags { get; set; }
        public required List<Guid> Assets { get; set; }
    }

    public class SetDetailsResponse
    {
        public required Guid Id { get; set; }
        public required string TenantId { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
        public required string? PriceDescription { get; set; }
        public required string Price { get; set; }
        public required bool IsVisible { get; set; }
        public required bool IsAvailable { get; set; }
        public required int? Quantity { get; set; }
        public required List<Guid> Products { get; set; }
        public required List<string> Tags { get; set; }
        public required List<Guid> Assets { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }


    // general product response
    public class ProductResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required bool IsAvailable { get; set; }
        public required bool IsVisible { get; set; }
        public required List<IngredientsResponse> Ingredients { get; set; }
        public required List<AllergensResponse> Allergens { get; set; }
        public required List<NutritionsResponse> Nutritions { get; set; }
        public required List<Guid> Assets { get; set; }
        public required List<string> Tags { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }
    public class ProductDetailsResponse
    {
        public required Guid Id { get; set; }
        public required string TenantId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int? Quantity { get; set; }
        public required bool IsAvailable { get; set; }
        public required bool IsVisible { get; set; }
        public string? Description { get; set; }
        public string? PriceDescription { get; set; }
        public required List<IngredientsResponse> Ingredients { get; set; }
        public required List<AllergensResponse> Allergens { get; set; }
        public required List<NutritionsResponse> Nutritions { get; set; }
        public required List<Guid> Assets { get; set; }
        public required List<string> Tags { get; set; }
        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
    }
    

    // ingredients response
    public class IngredientsResponse
    {
        public required string Name { get; set; }
    }
    public class AllergensResponse
    {
        public required string Name { get; set; }
        public required string Abbr { get; set; }
    }

    public class NutritionsResponse
    {
        public required string Name { get; set; }
        public required int Weight { get; set; }
    }
    #endregion




    // paginations
    public class PaginationResponse
    {
        public required int CurrentPage { get; set; }
        public required int TotalPages { get; set; }
        public required List<int> Pages { get; set; }
    }

    public class PaginatedCategoryResponse
    {
        public required PaginationResponse Pagination { get; set; }
        public required IReadOnlyCollection<CategoryResponse> Categories { get; set; }
    }

    public class PaginatedProductResponse
    {
        public required PaginationResponse Pagination { get; set; }
        public required IReadOnlyCollection<ProductResponse> Products { get; set; }
    }

    public class PaginatedSetResponse
    {
        public required PaginationResponse Pagination { get; set; }
        public required IReadOnlyCollection<SetResponse> Sets { get; set; }
    }
    #endregion
}
