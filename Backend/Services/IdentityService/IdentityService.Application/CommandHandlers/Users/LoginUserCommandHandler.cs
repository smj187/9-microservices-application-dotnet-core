using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ApplicationUser>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public async Task<ApplicationUser> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.LoginUserAsync(request.Email, request.Password);

            var token = await _authService.CreateJsonWebToken(request.Email);

            return user;
        }
    }
}
