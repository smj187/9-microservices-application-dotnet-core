using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1.Requests
{
    public class RevokeAuthenticationRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = default!;
    }
}
