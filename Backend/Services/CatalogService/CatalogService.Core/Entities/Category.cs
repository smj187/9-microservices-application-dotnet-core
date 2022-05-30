using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Interfaces;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Entities
{
    public class Category : AggregateRoot
    {

        private string _name;
        private string? _description;
        private List<Guid> _products;


        public Category(string name, string? descripion = null, List<Guid>? productIds = null)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            _name = name;
            _description = descripion;

            _products = productIds ?? new List<Guid>();
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        public List<Guid> Images { get; set; } = new();


        [BsonElement("Products")]
        public IEnumerable<Guid> ProductIds
        {
            get => _products;
            private set => _products = new List<Guid>(value);
        }

        public void AddProduct(Guid productId)
        {
            Guard.Against.Null(productId, nameof(productId));

            _products.Add(productId);
        }

        public void RemoveProduct(Guid productId)
        {
            Guard.Against.Null(productId, nameof(productId));

            _products.Remove(productId);
        }

        public void ChangeDescription(string name, string description)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _name = name;
            _description = description;
        }

    }
}
