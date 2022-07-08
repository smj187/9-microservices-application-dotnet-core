using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<InternalUserModel>
    {
        public string TenantId { get; set; } = default!;

        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;


        public string? Firstname { get; set; } = null;
        public string? Lastname { get; set; } = null;
    }
}
