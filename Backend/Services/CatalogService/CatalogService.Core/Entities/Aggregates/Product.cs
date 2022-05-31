using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Entities.Aggregates
{
    public class Product : AggregateRoot
    {
        private List<Ingredient> _ingredients;
        private List<Allergen> _allergens;
        private List<Nutrition> _nutritions;

        private decimal _price;
        private bool _isVisible;

        private string _name;
        private string? _description;
        private string? _priceDescription;
        private List<string>? _tags;

        public Product(string name, decimal price, IEnumerable<Ingredient>? ingredients = null, IEnumerable<Allergen>? allergens = null, IEnumerable<Nutrition>? nutritions = null, IEnumerable<string>? tags = null, string? description = null, string? priceDescription = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrNegativ(price, nameof(price));

            _name = name;
            _description = description;
            _priceDescription = priceDescription;
            _tags = tags?.ToList() ?? null;


            _ingredients = ingredients?.ToList() ?? new List<Ingredient>();
            _allergens = allergens?.ToList() ?? new List<Allergen>();
            _nutritions = nutritions?.ToList() ?? new List<Nutrition>();


            _price = price;
            _isVisible = false;

            Images = new List<Guid>();
        }


        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        public string? PriceDescription
        {
            get => _priceDescription;
            private set => _priceDescription = value;
        }


        public bool IsVisible
        {
            get => _isVisible;
            private set => _isVisible = value;
        }


        public List<Guid> Images { get; set; } = new();




        [BsonElement("Ingredients")]
        public IEnumerable<Ingredient> Ingredients
        {
            get => _ingredients;
            private set => _ingredients = new List<Ingredient>(value);
        }

        [BsonElement("Allergens")]
        public IEnumerable<Allergen> Allergens
        {
            get => _allergens;
            private set => _allergens = new List<Allergen>(value);
        }

        [BsonElement("Nutritions")]
        public IEnumerable<Nutrition> Nutritions
        {
            get => _nutritions;
            private set => _nutritions = new List<Nutrition>(value);
        }

        [BsonElement("Tags")]
        public IEnumerable<string>? Tags
        {
            get => _tags;
            private set => _tags = value == null ? null : new List<string>(value);
        }

        public void ChangeDescription(string name, string? description = null, string? priceDesription = null, List<string>? tags = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _name = name;
            _description = description;
            _priceDescription = priceDesription;
            _tags = tags;
        }

        public void ChangeVisibility(bool isVisible)
        {
            Guard.Against.Null(isVisible, nameof(isVisible));

            _isVisible = isVisible;
        }

        public void ChangePrice(decimal price)
        {
            Guard.Against.NullOrNegativ(price, nameof(price));

            _price = price;
        }


        public void AddIngredients(IEnumerable<Ingredient> ingredients)
        {
            Guard.Against.Null(ingredients, nameof(ingredients));

            foreach (var ingredient in ingredients)
            {
                if (!_ingredients.Contains(ingredient))
                {
                    _ingredients.Add(ingredient);
                }
            }
        }

        public void AddAllergens(IEnumerable<Allergen> allergens)
        {
            Guard.Against.Null(allergens, nameof(allergens));

            foreach (var allergen in allergens)
            {
                if (!_allergens.Contains(allergen))
                {
                    _allergens.Add(allergen);
                }
            }
        }

        public void AddNutrition(IEnumerable<Nutrition> nutritions)
        {
            Guard.Against.Null(nutritions, nameof(nutritions));

            foreach (var nutrition in nutritions)
            {
                if (!_nutritions.Contains(nutrition))
                {
                    _nutritions.Add(nutrition);
                }
            }
        }

        public void RemoveIngredients(IEnumerable<Ingredient> ingredients)
        {
            Guard.Against.Null(ingredients, nameof(ingredients));

            foreach (var ingredient in ingredients)
            {
                var existing = _ingredients.FirstOrDefault(x => x.Equals(ingredient));
                if (existing != null)
                {
                    _ingredients.Remove(existing);
                }
            }
        }
        public void RemoveAllergen(IEnumerable<Allergen> allergens)
        {
            Guard.Against.Null(allergens, nameof(allergens));

            foreach (var allergen in allergens)
            {
                var existing = _allergens.FirstOrDefault(x => x.Equals(allergen));
                if (existing != null)
                {
                    _allergens.Remove(existing);
                }
            }
        }
        public void RemoveNutrition(IEnumerable<Nutrition> nutritions)
        {
            Guard.Against.Null(nutritions, nameof(nutritions));

            foreach (var nutrition in nutritions)
            {
                var existing = _nutritions.FirstOrDefault(x => x.Equals(nutrition));
                if (existing != null)
                {
                    _nutritions.Remove(existing);
                }
            }
        }
    }
}
