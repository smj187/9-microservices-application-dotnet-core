using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, AuthenticatedUser>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public RevokeTokenCommandHandler(ITokenService authService, IUserService userService)
        {
            _tokenService = authService;
            _userService = userService;
        }

        public async Task<AuthenticatedUser> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindProfileAsync(request.UserId);
            if (user == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            await _userService.RevokeTokenAsync(request.Token);

            return new AuthenticatedUser(user, null, null, null);
        }
    }
}
