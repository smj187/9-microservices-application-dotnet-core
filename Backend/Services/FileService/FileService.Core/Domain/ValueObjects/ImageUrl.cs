using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.ValueObjects
{
    public class ImageUrl : ValueObject
    {
        private int _breakpoint;
        private string _url;
        private string _format;
        private long _size;
        private int _width;
        private int _height;

        protected ImageUrl() { }

        public ImageUrl(int breakpoint, string url, string format, long size, int width, int height)
        {
            Guard.Against.Null(breakpoint, nameof(breakpoint));
            Guard.Against.NullOrWhiteSpace(url, nameof(url));
            Guard.Against.NullOrWhiteSpace(format, nameof(format));
            Guard.Against.NegativeOrZero(size, nameof(size));
            Guard.Against.NegativeOrZero(width, nameof(width));
            Guard.Against.NegativeOrZero(height, nameof(height));

            _breakpoint = breakpoint;
            _url = url;
            _format = format;
            _size = size;
            _width = width;
            _height = height;
        }

        public int Breakpoint
        {
            get => _breakpoint;
            private set => _breakpoint = value;
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
            yield return Breakpoint;
            yield return Url;
            yield return Format;
            yield return Size;
            yield return Width;
            yield return Height;
        }
    }
}
