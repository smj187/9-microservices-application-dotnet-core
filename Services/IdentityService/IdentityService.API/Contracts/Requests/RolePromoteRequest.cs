using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Requests
{
    public class RolePromoteRequest
    {
        public Guid UserId { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
