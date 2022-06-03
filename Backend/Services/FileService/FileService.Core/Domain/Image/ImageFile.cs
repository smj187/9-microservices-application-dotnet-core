using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Image
{
    public class ImageFile : AggregateRoot
    {
        private Guid _externalEntityId;
        private string? _title;
        private string? _description;
        private string? _tags;

        private List<ImageUrl> _images = new();

        private ImageFile() { }

        public ImageFile(Guid externalEntityId, string? title = null, string? description = null, string? tags = null)
        {
            Guard.Against.Null(externalEntityId, nameof(externalEntityId));
            _externalEntityId = externalEntityId;

            _title = title;
            _description = description;
            _tags = tags;

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
        }


        public Guid ExternalEntityId
        {
            get => _externalEntityId;
            private set => _externalEntityId = value;
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

        public IEnumerable<ImageUrl> Images
        {
            get => _images;
            private set => _images = new List<ImageUrl>(value);
        }

        public void AddImages(List<ImageUrl> urls)
        {
            Guard.Against.NullOrEmpty(urls, nameof(urls));
            _images = urls;
        }

        public void ChangeDescription(string? title, string? description, string? tags)
        {
            _title = title;
            _description = description;
            _tags = tags;

            ModifiedAt = DateTimeOffset.UtcNow;
        }
    }
}
