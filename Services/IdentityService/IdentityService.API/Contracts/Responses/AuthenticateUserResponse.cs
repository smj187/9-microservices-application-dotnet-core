using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Responses
{
    public class AuthenticateUserResponse
    {
        public string Message { get; set; } = default!;
        public bool Success { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
        public string Jsonwebtoken { get; set; } = default!;
    }
}
