using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Users
{
    public class LoginUserCommand : IRequest<InternalUserModel>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
