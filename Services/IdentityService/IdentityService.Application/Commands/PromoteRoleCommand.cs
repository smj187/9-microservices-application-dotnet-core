using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class PromoteRoleCommand : IRequest<User>
    {
        public string Username { get; set; } = default!;
        public string NewRule { get; set; } = default!;
    }
}
