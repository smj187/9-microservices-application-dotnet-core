using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RevokeTokenCommand : IRequest<bool>
    {
        public string JsonWebToken { get; set; } = default!;
        public Guid UserId { get; set; }
    }
}
