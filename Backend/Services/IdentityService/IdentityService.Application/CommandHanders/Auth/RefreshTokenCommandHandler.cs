using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Auth;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHanders.Auth
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, InternalUserModel>
    {
        private readonly IUserService _userService;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(IUserService userService, IApplicationUserRepository applicationUserRepository, ITokenService tokenService)
        {
            _userService = userService;
            _applicationUserRepository = applicationUserRepository;
            _tokenService = tokenService;
        }

        public async Task<InternalUserModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var identityUser = await _userService.FindIdentityUser(request.UserId);
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            var token = await _tokenService.CreateJsonWebToken(identityUser.Email);

            applicationUser.RevokeRefreshToken(request.Token);

            var newRefreshToken = _tokenService.CreateRefreshToken();
            var expiresAt = newRefreshToken.ExpiresAt;
            var refreshToken = newRefreshToken.Token;

            applicationUser.AddRefreshToken(newRefreshToken);
            await _applicationUserRepository.PatchAsync(applicationUser);

            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                Token = token,
                ExpiresAt = expiresAt,
                RefreshToken = refreshToken
            };
        }
    }
}
