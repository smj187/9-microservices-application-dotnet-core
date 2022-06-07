using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using FileService.Core.Domain.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.User
{
    public class Avatar : AggregateRoot
    {
        private Guid _userId;
        private string _url = string.Empty;

        private Avatar() { }

        public Avatar(Guid userId, string url)
        {
            Guard.Against.Null(userId, nameof(userId));
            Guard.Against.Null(url, nameof(url));
            _userId = userId;
            _url = url;

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
        }

        public Guid UserId
        {
            get => _userId;
            private set => _userId = value;
        }

        public string Url 
        {
            get => _url;
            private set => _url = value;
        }
    }
}
