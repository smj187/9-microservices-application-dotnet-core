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
    public class UnlockUserAccountCommandHandler : IRequestHandler<UnlockUserAccountCommand, ApplicationUser>
    {
        private readonly IAdminService _adminService;

        public UnlockUserAccountCommandHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<ApplicationUser> Handle(UnlockUserAccountCommand request, CancellationToken cancellationToken)
        {
            return await _adminService.UnlockUserAccountAsync(request.UserId);
        }
    }
}
