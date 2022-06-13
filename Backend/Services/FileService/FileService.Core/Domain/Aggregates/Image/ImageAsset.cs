using Ardalis.GuardClauses;
using FileService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates.Image
{
    public class ImageAsset : AssetFile
    {
        private List<ImageUrl> _imageUrls;
        private string? _title;
        private string? _description;
        private string? _tags;

        // ef required (never called)
        public ImageAsset() 
            : base() 
        {
            _imageUrls = default!;
        }

        public ImageAsset(Guid externalEntityId, List<ImageUrl> imageUrls, AssetType assetType, string? title = null, string? description = null, string? tags = null)
            : base(externalEntityId, assetType, "image")
        {
            Guard.Against.NullOrEmpty(imageUrls, nameof(imageUrls));

            _imageUrls = imageUrls;
            _title = title;
            _description = description;
            _tags = tags;
        }




        public IEnumerable<ImageUrl> Images
        {
            get => _imageUrls;
            private set => _imageUrls = new List<ImageUrl>(value);
        }

        public string? Title
        {
            get => _title;
            private set => _title = value;
        }

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        public string? Tags
        {
            get => _tags;
            private set => _tags = value;
        }


        public override void PatchDescription(string? title, string? description, string? tags)
        {
            _title = title;
            _description = description;
            _tags = tags;

            ModifiedAt = DateTimeOffset.UtcNow;
        }
    }
}
