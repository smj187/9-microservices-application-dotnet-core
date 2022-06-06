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
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, ApplicationUser>
    {
        private readonly IAdminService _adminService;

        public RemoveRoleFromUserCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<ApplicationUser> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            return await _adminService.RemoveRoleFromUser(request.UserId, request.Roles);
        }
    }
}
