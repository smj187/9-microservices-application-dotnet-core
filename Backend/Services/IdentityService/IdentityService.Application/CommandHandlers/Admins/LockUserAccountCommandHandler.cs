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
    public class LockUserAccountCommandHandler : IRequestHandler<LockUserAccountCommand, ApplicationUser>
    {
        private readonly IAdminService _adminService;

        public LockUserAccountCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<ApplicationUser> Handle(LockUserAccountCommand request, CancellationToken cancellationToken)
        {
            return await _adminService.LockUserAccountAsync(request.UserId);
        }
    }
}
