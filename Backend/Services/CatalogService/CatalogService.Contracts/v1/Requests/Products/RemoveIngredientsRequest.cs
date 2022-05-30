using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Requests.Products
{
    public record RemoveIngredientsRequest(List<RemoveIngredientsIngredientsRequest> Ingredients, List<RemoveIngredientsAllergensRequest> Allergens, List<RemoveIngredientsNutritionsRequest> Nutritions);

    public record RemoveIngredientsIngredientsRequest([Required] string Name);
    public record RemoveIngredientsAllergensRequest([Required] string Abbr, [Required] string Name);
    public record RemoveIngredientsNutritionsRequest([Required] string Name, [Required] int Weight);
}
