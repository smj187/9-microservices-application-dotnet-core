using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates
{
    public abstract class AssetFile : AggregateRoot
    {
        private Guid _externalEntityId;
        private AssetType _assetType;
        private string _type;
    
        public AssetFile(Guid externalEntityId, AssetType assetType, string type)
        {
            Guard.Against.Null(externalEntityId, nameof(externalEntityId));
            Guard.Against.Null(assetType, nameof(assetType));
            Guard.Against.NullOrEmpty(type, nameof(type));
            _externalEntityId = externalEntityId;
            _assetType = assetType;

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


        public abstract void PatchDescription(string? title, string? description, string? tags);

    }
}
