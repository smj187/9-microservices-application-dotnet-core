using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Auth
{
    public class LoginExistingUserCommand : IRequest<InternalUserModel>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
