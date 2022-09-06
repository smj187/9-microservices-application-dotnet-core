using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, InternalUserModel>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public LoginUserCommandHandler(IUserService userService, ITokenService authService, IApplicationUserRepository applicationUserRepository)
        {
            _userService = userService;
            _tokenService = authService;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<InternalUserModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = await _userService.LoginUserAsync(request.Email, request.Password);
            var applicationUser = await _applicationUserRepository.FindAsync(identityUser.Id);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), identityUser.Id);
            }

            var token = await _tokenService.CreateJsonWebToken(request.Email);


            DateTimeOffset expiresAt;
            string refreshToken;

            var activeRefreshToken = applicationUser.GetActiveRefreshToken();
            if (activeRefreshToken != null)
            {
                expiresAt = activeRefreshToken.ExpiresAt;
                refreshToken = activeRefreshToken.Token;
            }
            else
            {
                var newRefreshToken = _tokenService.CreateRefreshToken();
                expiresAt = newRefreshToken.ExpiresAt;
                refreshToken = newRefreshToken.Token;

                applicationUser.AddRefreshToken(newRefreshToken);
                await _applicationUserRepository.PatchAsync(applicationUser);
            }


            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Token = token,
                ExpiresAt = expiresAt,
                RefreshToken = refreshToken
            };
        }
    }
}
