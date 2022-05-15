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
    public class RefreshAuthenticationCommandHandler : IRequestHandler<RefreshAuthenticationCommand, Token>
    {
        private readonly IIdentityService _identityService;

        public RefreshAuthenticationCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Token> Handle(RefreshAuthenticationCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.RefreshAuthenticationAsync(request.Token);
        }
    }
}
