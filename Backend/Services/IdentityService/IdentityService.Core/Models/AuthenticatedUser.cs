using IdentityService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Models
{
    public record AuthenticatedUser(ApplicationUser User, string? Token = null, string? RefreshToken = null, DateTimeOffset? RefreshTokenExpiration = null);
}
