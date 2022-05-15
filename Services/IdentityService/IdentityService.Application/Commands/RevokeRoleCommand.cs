using IdentityService.Core.Entities;
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
        public Guid UserId { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
