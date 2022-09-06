using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _authService;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly ITokenService _tokenService;

        public RevokeTokenCommandHandler(IUserService userService, ITokenService authService, IApplicationUserRepository applicationUserRepository, ITokenService tokenService)
        {
            _userService = userService;
            _authService = authService;
            _applicationUserRepository = applicationUserRepository;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);
            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            applicationUser.RevokeRefreshToken(request.Token);

            await _applicationUserRepository.PatchAsync(applicationUser);

            return Unit.Value;
        }
    }
}
