using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Domain.Categories
{
    public class Category : AggregateBase, IMultitenantAggregate
    {
        private Guid _id;

        private string _name;
        private string? _description;
        private List<Guid> _products;
        private List<Guid> _sets;
        private bool _isVisible;
        private List<Guid> _assets;
        private string _tenantId;

        public Category(string tenantId, Guid id, string name, string? descripion = null, List<Guid>? products = null, List<Guid>? sets = null)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _id = id;
            _tenantId = tenantId;
            _name = name;
            _description = descripion;

            _products = products ?? new List<Guid>();
            _sets = sets ?? new List<Guid>();

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

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        public bool IsVisible
        {
            get => _isVisible;
            private set => _isVisible = value;
        }

        public List<Guid> Assets
        {
            get => _assets;
            private set => _assets = value;
        }

        public void AddAssetId(Guid imageId)
        {
            Guard.Against.Null(imageId, nameof(imageId));
            _assets.Add(imageId);
        }


        public IEnumerable<Guid> Products
        {
            get => _products;
            private set => _products = new List<Guid>(value);
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

        public IEnumerable<Guid> Sets
        {
            get => _sets;
            private set => _sets = new List<Guid>(value);
        }



        public void AddSet(Guid setId)
        {
            Guard.Against.Null(setId, nameof(setId));

            _sets.Add(setId);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveSet(Guid setId)
        {
            Guard.Against.Null(setId, nameof(setId));

            _sets.Remove(setId);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ChangeDescription(string name, string? description)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));

            _name = name;
            _description = description;

            ModifiedAt = DateTimeOffset.UtcNow;
        }


        public void ChangeVisibility(bool isVisible)
        {
            Guard.Against.Null(isVisible, nameof(isVisible));

            _isVisible = isVisible;

            ModifiedAt = DateTimeOffset.UtcNow;
        }

    }
}