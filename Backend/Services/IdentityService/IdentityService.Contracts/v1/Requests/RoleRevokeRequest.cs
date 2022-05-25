using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1.Requests
{
    public class RoleRevokeRequest
    {
        public Guid UserId { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
