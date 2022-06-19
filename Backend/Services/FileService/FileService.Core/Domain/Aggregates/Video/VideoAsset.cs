using Ardalis.GuardClauses;
using FileService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates.Video
{
    public class VideoAsset : AssetFile
    {
        private VideoUrl _videoUrl;
        private string? _title;
        private string? _description;
        private string? _tags;

        // ef required (never called)
        public VideoAsset() 
            : base() 
        {
            _videoUrl = default!;
        }

        public VideoAsset(Guid externalEntityId, VideoUrl videoUrl, AssetType assetType, string? title, string? description, string? tags)
            : base(externalEntityId, assetType, "video")
        {
            Guard.Against.Null(videoUrl, nameof(videoUrl));

            _videoUrl = videoUrl;
            _title = title;
            _description = description;
            _tags = tags;
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

        public VideoUrl Video
        {
            get => _videoUrl;
            private set => _videoUrl = value;
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
