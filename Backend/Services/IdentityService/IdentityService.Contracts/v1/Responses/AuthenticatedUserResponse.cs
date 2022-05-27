using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1.Responses
{
    public class AuthenticatedUserResponse
    {
        public string Token { get; set; }

        public AuthenticatedUserUserResponse User { get; set; }
    }

    public class AuthenticatedUserUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

    }
}
