using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandsHandler
{
    public class RevokeRoleCommandHandler : IRequestHandler<RevokeRoleCommand, User>
    {
        private readonly IUserService _userService;

        public RevokeRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(RevokeRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RevokeRoleAsync(request.UserId, request.Role);
        }
    }
}
