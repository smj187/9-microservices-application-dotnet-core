using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Group
{
    public class Group : AggregateRoot
    {

        private string _name;
        private decimal _price;
        private string? _description;
        private string? _priceDescription;
        private bool _isVisible;
        private List<string>? _tags;
        private List<Guid> _products;

        public Group(string name, decimal price, string? description = null, string? priceDescription = null, IEnumerable<string>? tags = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrNegativ(price, nameof(price));

            _name = name;
            _price = price;

            _description = description;
            _priceDescription = priceDescription;
            _tags = tags?.ToList() ?? null;

            _isVisible = false;
            _products = new List<Guid>();
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



        [BsonElement("Products")]
        public IEnumerable<Guid> Products
        {
            get => _products;
            private set => _products = new List<Guid>(value);
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

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeVisibility(bool isVisible)
        {
            Guard.Against.Null(isVisible, nameof(isVisible));

            _isVisible = isVisible;

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangePrice(decimal price)
        {
            Guard.Against.NullOrNegativ(price, nameof(price));

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
