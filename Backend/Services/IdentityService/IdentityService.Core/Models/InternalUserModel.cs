using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Models
{
    public class InternalUserModel
    {
        public required ApplicationUser ApplicationUser { get; set; }
        public InternalIdentityUser? InternalIdentityUser { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }
}
