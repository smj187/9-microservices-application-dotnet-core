using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Video
{
    public class VideoUrl : ValueObject
    {
        private string _url;
        private string _format;
        private long _size;
        private double _duration;
        private int _width;
        private int _height;

        public VideoUrl(string url, string format, double duration, long size, int width, int height)
        {
            Guard.Against.NullOrEmpty(url, nameof(url));
            Guard.Against.NullOrWhiteSpace(format, nameof(format));
            Guard.Against.Null(duration, nameof(duration));
            Guard.Against.NullOrNegativ(size, nameof(size));
            Guard.Against.NullOrNegativ(width, nameof(width));
            Guard.Against.NullOrNegativ(height, nameof(height));

            _url = url;
            _format = format;
            _duration = duration;
            _size = size;
            _width = width;
            _height = height;
        }

        public string Url
        {
            get => _url;
            private set => _url = value;
        }

        public string Format
        {
            get => _format;
            private set => _format = value;
        }

        public double Duration
        {
            get => _duration;
            private set => _duration = value;
        }

        public long Size
        {
            get => _size;
            private set => _size = value;
        }

        public int Width
        {
            get => _width;
            private set => _width = value;
        }

        public int Height
        {
            get => _height;
            private set => _height = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Url;
            yield return Format;
            yield return Size;
            yield return Width;
            yield return Height;
        }
    }
}
