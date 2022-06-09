using BuildingBlocks.Exceptions;
using IdentityService.Application.Queries.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class FindUserProfileQueryHandler : IRequestHandler<FindUserProfileQuery, ApplicationUser>
    {
        private readonly IUserService _userService;

        public FindUserProfileQueryHandler(IUserService userService)
        {
            _userService = userService;
        } 

        public async Task<ApplicationUser> Handle(FindUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindProfileAsync(request.UserId);
            if (user == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            return user;
        }
    }
}
