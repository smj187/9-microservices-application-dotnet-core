using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Token>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Token> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var token = await _userService.AuthenticateAsync(request.Token);

            return token;
        }
    }
}
