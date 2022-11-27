using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Auth
{
    public class RevokeTokenCommand : IRequest
    {
        public required Guid UserId { get; set; }
        public required string Token { get; set; }
    }
}
