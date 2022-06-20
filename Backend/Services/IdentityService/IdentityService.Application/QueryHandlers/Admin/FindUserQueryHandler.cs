using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Queries.Admins;
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

namespace IdentityService.Application.QueryHandlers.Admin
{
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, InternalUserModel>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public FindUserQueryHandler(IApplicationUserRepository applicationUserRepository, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var all = await _applicationUserRepository.ListAsync();
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var identityUser = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalUserModel), request.UserId);
            }

            var roles = await _userManager.GetRolesAsync(identityUser);

            var responseUser = new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Roles = roles.ToList()
            };

            return responseUser;
        }
    }
}
