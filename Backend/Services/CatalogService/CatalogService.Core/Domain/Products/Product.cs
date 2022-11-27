using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Products
{
    public class Product : AggregateBase
    {
        private Guid _id;

        private List<Ingredient> _ingredients;
        private List<Allergen> _allergens;
        private List<Nutrition> _nutritions;

        private decimal _price;
        private bool _isVisible;
        private bool _isAvailable;

        private string _name;
        private string? _description;
        private string? _priceDescription;
        private List<string>? _tags;
        private List<Guid> _assets;
        private int? _quantity;
        private string _tenantId;

        public Product(string tenantId, Guid id, string name, decimal price, IEnumerable<Ingredient>? ingredients = null, IEnumerable<Allergen>? allergens = null, IEnumerable<Nutrition>? nutritions = null, IEnumerable<string>? tags = null, string? description = null, string? priceDescription = null, int? quantity = null)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NegativeOrZero(price, nameof(price));

            _tenantId = tenantId;
            _id = id;
            _name = name;
            _description = description;
            _priceDescription = priceDescription;
            _tags = tags?.ToList() ?? null;
            _quantity = quantity;


            _ingredients = ingredients?.ToList() ?? new List<Ingredient>();
            _allergens = allergens?.ToList() ?? new List<Allergen>();
            _nutritions = nutritions?.ToList() ?? new List<Nutrition>();


            _price = price;
            _isVisible = false;
            _isAvailable = false;

            _assets = new();
        }

        public override Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        public string TenantId
        {
            get => _tenantId;
            set => _tenantId = Guard.Against.NullOrWhiteSpace(value, nameof(value));
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

        public bool IsAvailable
        {
            get => _isAvailable;
            private set => _isAvailable = value;
        }

        public int? Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public List<Guid> Assets
        {
            get => _assets;
            private set => _assets = value;
        }

        public IEnumerable<Ingredient> Ingredients
        {
            get => _ingredients;
            private set => _ingredients = new List<Ingredient>(value);
        }

        public IEnumerable<Allergen> Allergens
        {
            get => _allergens;
            private set => _allergens = new List<Allergen>(value);
        }

        public IEnumerable<Nutrition> Nutritions
        {
            get => _nutritions;
            private set => _nutritions = new List<Nutrition>(value);
        }

        public IEnumerable<string>? Tags
        {
            get => _tags;
            private set => _tags = value == null ? null : new List<string>(value);
        }



        public void AddAssetId(Guid imageId)
        {
            Guard.Against.Null(imageId, nameof(imageId));
            _assets.Add(imageId);

            Modify();
        }

        public void ChangeDescription(string name, string? description = null, string? priceDesription = null, List<string>? tags = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _name = name;
            _description = description;
            _priceDescription = priceDesription;
            _tags = tags;

            Modify();
        }

        public void ChangeVisibility(bool isVisible)
        {
            Guard.Against.Null(isVisible, nameof(isVisible));

            _isVisible = isVisible;

            Modify();
        }

        public void ChangeQuantity(int? quantity)
        {
            _quantity = quantity;

            Modify();
        }

        public bool DecreaseQuantity()
        {
            if (_quantity <= 0) return false;

            _quantity = Quantity - 1;

            return true;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            Guard.Against.Null(isAvailable, nameof(isAvailable));

            _isAvailable = isAvailable;

            Modify();
        }

        public void ChangePrice(decimal price)
        {
            Guard.Against.NegativeOrZero(price, nameof(price));

            _price = price;

            Modify();
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

            Modify();
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

            Modify();
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

            Modify();
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

            Modify();
        }
        public void RemoveAllergen(IEnumerable<Allergen> allergens)
        {
            Guard.Against.Null(allergens, nameof(allergens));

            foreach (var allergen in allergens)
            {
                var existing = _allergens.FirstOrDefault(x => x.Equals(allergen)) ?? null;
                if (existing != null)
                {
                    _allergens.Remove(existing);
                }
            }

            Modify();
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

            Modify();
        }
    }
}