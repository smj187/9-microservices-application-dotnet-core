using BuildingBlocks.Exceptions;
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
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ApplicationUser>
    {
        private readonly IUserService _userService;

        public AddRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApplicationUser> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AddRoleToUser(request.UserId, request.Roles);
        }
    }
}
