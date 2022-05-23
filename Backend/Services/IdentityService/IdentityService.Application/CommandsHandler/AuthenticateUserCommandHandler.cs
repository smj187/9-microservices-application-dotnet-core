using IdentityService.Application.Commands;
using IdentityService.Application.Services;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandsHandler
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Token>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Token> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.AuthenticateAsync(request.Token);
        }
    }
}
