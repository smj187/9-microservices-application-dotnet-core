using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandsHandler
{
    public class TerminateUserCommandHandler : IRequestHandler<TerminateUserCommand, bool>
    {
        private readonly IUserService _userService;

        public TerminateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(TerminateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.TerminateUserAsync(request.UserId);
        }
    }
}
