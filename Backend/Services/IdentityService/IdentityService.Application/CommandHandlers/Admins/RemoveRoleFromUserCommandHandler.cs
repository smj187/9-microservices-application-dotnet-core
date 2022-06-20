using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Admins;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Admins
{
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, InternalUserModel>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IAdminService _adminService;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public RemoveRoleFromUserCommandHandler(IApplicationUserRepository applicationUserRepository, IAdminService adminService, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _adminService = adminService;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);

            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var identityUser = await _adminService.RemoveRoleFromUser(request.UserId, request.Roles);

            var roles = await _userManager.GetRolesAsync(identityUser);


            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Roles = roles.ToList()
            };
        }
    }
}
