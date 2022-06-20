using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    //public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticatedUser>
    //{
    //    private readonly IUserService _userService;
    //    private readonly ITokenService _tokenService;

    //    public RefreshTokenCommandHandler(IUserService userService, ITokenService authService)
    //    {
    //        _userService = userService;
    //        _tokenService = authService;
    //    }

    //    public async Task<AuthenticatedUser> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    //    {
    //        var user = await _userService.FindProfileAsync(request.UserId);
    //        if (user == null)
    //        {
    //            throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
    //        }

    //        var refresh = await _userService.RenewRefreshTokenAsync(request.UserId, request.Token);

    //        var token = await _tokenService.CreateJsonWebToken(user.Email);
            

    //        return new AuthenticatedUser(user, token, refresh.Token, refresh.ExpiresAt);
    //    }
    //}
}
