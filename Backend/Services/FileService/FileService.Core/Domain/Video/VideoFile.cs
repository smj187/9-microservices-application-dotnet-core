using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Video
{
    public class VideoFile : AggregateRoot
    {
        private Guid _externalEntityId;
        private string? _title;
        private string? _description;
        private string? _tags;
        private VideoUrl _url;

        public VideoFile(Guid externalEntityId, string? title, string? description, string? tags)
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

        public VideoUrl Url
        {
            get => _url;
            private set => _url = value;
        }

        public void AddVideo(VideoUrl url)
        {
            Guard.Against.Null(url, nameof(url));

            _url = url;
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
