using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticatedUser>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthenticateUserCommandHandler(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<AuthenticatedUser> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.LoginUserAsync(request.Email, request.Password);
            var token = await _authService.CreateJsonWebToken(request.Email);

            return new AuthenticatedUser(user, token);
        }
    }
}
