using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Queries;
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

namespace IdentityService.Application.QueryHandlers
{
    public class FindUserProfileQueryHandler : IRequestHandler<FindUserProfileQuery, InternalUserModel>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public FindUserProfileQueryHandler(IApplicationUserRepository applicationUserRepository, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(FindUserProfileQuery request, CancellationToken cancellationToken)
        {
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

            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Roles = roles.ToList()
            };
        }
    }
}
