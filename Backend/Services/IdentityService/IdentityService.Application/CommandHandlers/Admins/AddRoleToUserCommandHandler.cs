using BuildingBlocks.Exceptions;
using IdentityService.Application.Commands.Admins;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Admins
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, ApplicationUser>
    {
        private readonly IAdminService _adminService;

        public AddRoleToUserCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<ApplicationUser> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return await _adminService.AddRoleToUser(request.UserId, request.Roles);
        }
    }
}
