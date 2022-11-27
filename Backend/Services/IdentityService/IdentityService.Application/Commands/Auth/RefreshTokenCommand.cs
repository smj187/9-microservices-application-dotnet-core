using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<InternalUserModel>
    {
        public required Guid UserId { get; set; }

        public required string Token { get; set; }
    }
}
