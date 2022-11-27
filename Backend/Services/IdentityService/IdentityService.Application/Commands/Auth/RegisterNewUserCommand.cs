using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Auth
{
    public class RegisterNewUserCommand : IRequest<InternalUserModel>
    {
        public required string TenantId { get; set; }

        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }


        public string? Firstname { get; set; } = null;
        public string? Lastname { get; set; } = null;
    }
}
