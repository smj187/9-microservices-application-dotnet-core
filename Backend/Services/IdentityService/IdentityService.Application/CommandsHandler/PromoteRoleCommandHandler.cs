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
    public class PromoteRoleCommandHandler : IRequestHandler<PromoteRoleCommand, User>
    {
        private readonly IUserService _userService;

        public PromoteRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(PromoteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.PromoteRoleAsync(request.UserId, request.Role);
        }
    }
}
