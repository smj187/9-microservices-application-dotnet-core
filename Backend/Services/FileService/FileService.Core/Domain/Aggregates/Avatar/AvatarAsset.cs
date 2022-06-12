using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates.Avatar
{
    public class AvatarAsset : AssetFile
    {
        private string _url;


        public AvatarAsset(Guid externalEntityId, string url, AssetType assetType)
            : base(externalEntityId, assetType, "image")
        {
            Guard.Against.NullOrWhiteSpace(url, nameof(url));
            Guard.Against.Null(assetType, nameof(assetType));

            _url = url;
        }

        public string Url
        {
            get => _url;
            private set => _url = value;
        }

        public override void PatchDescription(string? title, string? description, string? tags)
        {
            throw new NotImplementedException();
        }
    }
}
