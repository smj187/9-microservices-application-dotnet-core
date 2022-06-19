using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthenticatedUser>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(IUserService userService, ITokenService authService)
        {
            _userService = userService;
            _tokenService = authService;
        }

        public async Task<AuthenticatedUser> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.RegisterUserAsync(request.Username, request.Email, request.Password, request.Firstname, request.Lastname);
            var token = await _tokenService.CreateJsonWebToken(request.Email);

            var refresh = await _userService.CreateRefreshTokenAsync(user);

            return new AuthenticatedUser(user, token, refresh.Token, refresh.ExpiresAt);
        }
    }
}
