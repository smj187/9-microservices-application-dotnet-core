using BuildingBlocks.Exceptions;
using IdentityService.Application.Queries.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers.Users
{
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, ApplicationUser>
    {
        private readonly IUserService _userService;

        public FindUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApplicationUser> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindUserAsync(request.UserId);
            if (user == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            return user;
        }
    }
}
