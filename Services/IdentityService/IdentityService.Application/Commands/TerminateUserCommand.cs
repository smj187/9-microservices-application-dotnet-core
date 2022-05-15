using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class TerminateUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}
