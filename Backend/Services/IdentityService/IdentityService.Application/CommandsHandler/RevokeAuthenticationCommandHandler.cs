using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandsHandler
{
    public class RevokeAuthenticationCommandHandler : IRequestHandler<RevokeAuthenticationCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public RevokeAuthenticationCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(RevokeAuthenticationCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RevokeRefreshTokenAsync(request.UserId, request.RefreshToken);
        }
    }
}
