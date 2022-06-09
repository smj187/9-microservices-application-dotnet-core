using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Admins
{
    public class AddRoleToUserCommand : IRequest<ApplicationUser>
    {
        public Guid UserId { get; set; }

        public List<string> Roles { get; set; } = new();
    }
}
