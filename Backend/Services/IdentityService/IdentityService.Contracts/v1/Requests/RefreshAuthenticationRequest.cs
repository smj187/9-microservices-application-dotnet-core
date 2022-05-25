using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1.Requests
{
    public class RefreshAuthenticationRequest
    {
        public string RefreshToken { get; set; } = default!;
    }
}
