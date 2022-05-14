using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RegisterUserCommand : IRequest<User>
    {
        public User User { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
