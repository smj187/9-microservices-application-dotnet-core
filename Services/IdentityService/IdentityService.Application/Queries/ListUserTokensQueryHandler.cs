using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class ListUserTokensQueryHandler : IRequestHandler<ListUserTokensQuery, IReadOnlyCollection<RefreshToken>>
    {
        private readonly IUserService _userService;

        public ListUserTokensQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IReadOnlyCollection<RefreshToken>> Handle(ListUserTokensQuery request, CancellationToken cancellationToken)
        {
            return await _userService.ListUserTokensAsync(request.UserId);
        }
    }
}
