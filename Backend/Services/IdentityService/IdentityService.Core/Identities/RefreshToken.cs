using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Identities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = default!;
        public DateTimeOffset ExpiresAt { get; set; }
        public bool IsExpired => DateTimeOffset.UtcNow >= ExpiresAt;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
        public bool IsActive => RevokedAt == null && !IsExpired;
    }
}
