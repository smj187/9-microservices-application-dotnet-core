using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHandlers.Users
{
    public class PatchUserProfileCommandHandler : IRequestHandler<PatchUserProfileCommand, ApplicationUser>
    {
        private readonly IUserService _userService;

        public PatchUserProfileCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApplicationUser> Handle(PatchUserProfileCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserProfileAsync(request.UserId, request.Firstname, request.Lastname);
        }
    }
}
