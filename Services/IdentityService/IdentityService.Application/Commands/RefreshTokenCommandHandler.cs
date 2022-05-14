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
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Token>
    {
        private readonly IUserService _userService;

        public RefreshTokenCommandHandler(Services.IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Token> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _userService.RefreshTokenAsync(request.OldToken);
            return token;
        }
    }
}
