using CatalogService.Core.Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Commands.Products
{
    public class AddIngredientsToProductCommand : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Allergen> Allergens { get; set; } = new();
        public List<Nutrition> Nutritions { get; set; } = new();
    }
}
