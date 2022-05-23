using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Responses
{
    public class AuthenticateUserResponse
    {
        public Guid UserId { get; set; }
        public string? Message { get; set; } = null;
        public bool Success { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
        public string Token { get; set; } = default!;

        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiration { get; set; } = null;
    }
}
