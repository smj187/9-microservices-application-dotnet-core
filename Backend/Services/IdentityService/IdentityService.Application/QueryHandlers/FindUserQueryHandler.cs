using IdentityService.Application.Queries;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers
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
            return await _userService.FindUserAsync(request.UserId);
        }
    }
}
