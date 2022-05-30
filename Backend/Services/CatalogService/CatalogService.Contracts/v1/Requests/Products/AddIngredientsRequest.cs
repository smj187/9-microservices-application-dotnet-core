using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Products
{
    public record AddIngredientsRequest(List<AddIngredientsIngredientsRequest> Ingredients, List<AddIngredientsAllergensRequest> Allergens, List<AddIngredientsNutritionsRequest> Nutritions);

    public record AddIngredientsIngredientsRequest([Required] string Name);
    public record AddIngredientsAllergensRequest([Required] string Abbr, [Required] string Name);
    public record AddIngredientsNutritionsRequest([Required] string Name, [Required] int Weight);
}
