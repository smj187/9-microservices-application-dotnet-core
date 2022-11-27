﻿using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.User;
using IdentityService.Application.Services.Interfaces;
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

namespace IdentityService.Application.CommandHanders.User
{
    public class UnlockUserAccountCommandHandler : IRequestHandler<UnlockUserAccountCommand, InternalUserModel>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IAdminService _adminService;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public UnlockUserAccountCommandHandler(IApplicationUserRepository applicationUserRepository, IAdminService adminService, UserManager<Core.Identities.InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _adminService = adminService;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(UnlockUserAccountCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);

            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var identityUser = await _adminService.UnlockUserAccountAsync(applicationUser.Id);

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