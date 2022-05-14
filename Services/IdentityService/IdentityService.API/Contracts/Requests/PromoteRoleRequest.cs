using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Requests
{
    public class PromoteRoleRequest
    {
        public string Username { get; set; } = default!;
        public string NewRole { get; set; } = default!;
    }
}
