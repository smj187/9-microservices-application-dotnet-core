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
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IReadOnlyCollection<User>>
    {
        private readonly IUserService _userService;

        public ListUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IReadOnlyCollection<User>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _userService.ListUsersAsync();
        }
    }
}
