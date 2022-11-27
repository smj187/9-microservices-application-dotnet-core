using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Auth;
using IdentityService.Application.Services;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHanders.Auth
{
    public class LoginExistingUserCommandHandler : IRequestHandler<LoginExistingUserCommand, InternalUserModel>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public LoginExistingUserCommandHandler(IUserService userService, ITokenService tokenService, IApplicationUserRepository applicationUserRepository, UserManager<InternalIdentityUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(LoginExistingUserCommand request, CancellationToken cancellationToken)
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

            var roles = await _userManager.GetRolesAsync(identityUser);

            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Token = token,
                ExpiresAt = expiresAt,
                RefreshToken = refreshToken,
                Roles = roles.ToList(),
            };
        }
    }
}
