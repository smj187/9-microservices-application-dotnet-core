using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Requests
{
    public class RevokeTokenRequest
    {
        public Guid UserId { get; set; }
        public string JsonWebToken { get; set; } = default!;
    }
}
