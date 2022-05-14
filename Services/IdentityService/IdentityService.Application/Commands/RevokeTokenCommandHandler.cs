using IdentityService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, bool>
    {
        private readonly IUserService _userService;

        public RevokeTokenCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RevokeRefreshTokenAsync(request.UserId, request.JsonWebToken);
        }
    }
}
