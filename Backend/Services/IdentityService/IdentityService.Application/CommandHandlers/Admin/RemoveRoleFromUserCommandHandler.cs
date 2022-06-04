using IdentityService.Application.Commands.Admin;
using IdentityService.Application.Services;
using IdentityService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Admin
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, ApplicationUser>
    {
        private readonly IUserService _userService;

        public RemoveRoleFromUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApplicationUser> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RemoveRoleFromUser(request.UserId, request.Roles);
        }
    }
}
