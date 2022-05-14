using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Models
{
    public class Token
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string JsonWebToken { get; set; } = default!;
        public string Message { get; set; } = default!;
        public bool Success { get; set; }
        public List<string> Roles { get; set; } = default!;
    }
}
