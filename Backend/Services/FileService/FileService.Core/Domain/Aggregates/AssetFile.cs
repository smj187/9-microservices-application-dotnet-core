using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates
{
    public abstract class AssetFile : AggregateBase, IMultitenantAggregate
    {
        private Guid _externalEntityId;
        private AssetType _assetType;
        private string _type;
        private string _tenantId;

        // ef required (never called)
        public AssetFile() 
        {
            _assetType = default!;
            _type = default!;
            _tenantId = default!;
        }
    
        public AssetFile(Guid externalEntityId, AssetType assetType, string tenantId, string type)
        {
            Guard.Against.Null(externalEntityId, nameof(externalEntityId));
            Guard.Against.Null(assetType, nameof(assetType));
            Guard.Against.NullOrEmpty(type, nameof(type));
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            _externalEntityId = externalEntityId;
            _assetType = assetType;
            _tenantId = tenantId;

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
            _type = type;
        }

        public Guid ExternalEntityId
        {
            get => _externalEntityId;
            private set => _externalEntityId = value;
        }

        public AssetType AssetType
        {
            get => _assetType;
            private set => _assetType = value;
        }

        public string Type
        {
            get => _type;
            private set => _type = value;
        }
        public string TenantId
        {
            get => _tenantId;
            set => _tenantId = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        public abstract void PatchDescription(string? title, string? description, string? tags);

    }
}
