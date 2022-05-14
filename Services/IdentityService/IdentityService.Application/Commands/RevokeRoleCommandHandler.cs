using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
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
            var user = await _userService.RevokeRoleAsync(request.Username, request.RoleToRemove);
            return user;
        }
    }
}
