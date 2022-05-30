using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Products
{
    public record CreateProductRequest([Required] string Name, string Description, [Required] decimal Price, string PriceDescription, [Required] List<CreateProductIngredientsRequest> Ingredients, [Required] List<CreateProductAllergensRequest> Allergens, [Required] List<CreateProductNutritionsRequest> Nutritions, List<string> Tags);
    public record CreateProductIngredientsRequest([Required] string Name);
    public record CreateProductAllergensRequest([Required] string Abbr, [Required] string Name);
    public record CreateProductNutritionsRequest([Required] string Name, [Required] int Weight);
}
