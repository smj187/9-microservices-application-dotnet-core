using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Sets
{
    public class Set : AggregateBase
    {
        private Guid _id;

        private string _name;
        private decimal _price;
        private string? _description;
        private string? _priceDescription;
        private bool _isVisible;
        private bool _isAvailable;
        private List<string>? _tags;
        private List<Guid> _products;
        private List<Guid> _assets;
        private int? _quantity;
        private string _tenantId;

        public Set(string tenantId, Guid id, string name, decimal price, string? description = null, string? priceDescription = null, IEnumerable<string>? tags = null, int? quantity = null)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NegativeOrZero(price, nameof(price));

            _id = id;
            _tenantId = tenantId;
            _name = name;
            _price = price;

            _description = description;
            _priceDescription = priceDescription;
            _tags = tags?.ToList() ?? null;
            _quantity = quantity;

            _products = new List<Guid>();
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

        public IEnumerable<Guid> Products
        {
            get => _products;
            private set => _products = new List<Guid>(value);
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

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeDescription(string name, string? description = null, string? priceDesription = null, List<string>? tags = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _name = name;
            _description = description;
            _priceDescription = priceDesription;
            _tags = tags;

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeVisibility(bool isVisible)
        {
            Guard.Against.Null(isVisible, nameof(isVisible));

            _isVisible = isVisible;

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeQuantity(int? quantity)
        {
            _quantity = quantity;

            ModifiedAt = DateTimeOffset.UtcNow;
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

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangePrice(decimal price)
        {
            Guard.Against.NegativeOrZero(price, nameof(price));

            _price = price;

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void AddProduct(Guid productId)
        {
            Guard.Against.Null(productId, nameof(productId));

            _products.Add(productId);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveProduct(Guid productId)
        {
            Guard.Against.Null(productId, nameof(productId));

            _products.Remove(productId);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

    }
}
