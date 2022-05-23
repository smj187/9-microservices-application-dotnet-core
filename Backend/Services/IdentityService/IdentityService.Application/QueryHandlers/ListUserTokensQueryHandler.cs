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
    public class ListUserTokensQueryHandler : IRequestHandler<ListUserTokensQuery, IReadOnlyCollection<RefreshToken>>
    {
        private readonly IIdentityService _identityService;

        public ListUserTokensQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IReadOnlyCollection<RefreshToken>> Handle(ListUserTokensQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.ListUserTokensAsync(request.UserId);
        }
    }
}
