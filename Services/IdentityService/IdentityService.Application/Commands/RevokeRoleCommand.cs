using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RevokeRoleCommand : IRequest<User>
    {
        public string Username { get; set; } = default!;
        public string RoleToRemove { get; set; } = default!;
    }
}
