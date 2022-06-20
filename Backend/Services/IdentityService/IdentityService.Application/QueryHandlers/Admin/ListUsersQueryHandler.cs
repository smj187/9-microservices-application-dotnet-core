using IdentityService.Application.Queries.Admins;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Application.QueryHandlers.Admin
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IReadOnlyCollection<InternalUserModel>>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public ListUsersQueryHandler(IApplicationUserRepository applicationUserRepository, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        public async Task<IReadOnlyCollection<InternalUserModel>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var applicationUsers = await _applicationUserRepository.ListAsync();

            var responseUsers = new List<InternalUserModel>();

            foreach (var applicationUser in applicationUsers)
            {
                var identityUser = await _userManager.FindByIdAsync(applicationUser.InternalUserId.ToString());
                var roles = await _userManager.GetRolesAsync(identityUser);

                responseUsers.Add(new InternalUserModel
                {
                    ApplicationUser = applicationUser,
                    InternalIdentityUser = identityUser,
                    Roles = roles.ToList()
                });
            }

            return responseUsers;
        }
    }
}
