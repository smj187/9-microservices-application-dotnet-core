using BuildingBlocks.EfCore.Interfaces;
using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Commands.User;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.CommandHanders.User
{
    public class PatchUserProfileCommandHandler : IRequestHandler<PatchUserProfileCommand, InternalUserModel>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public PatchUserProfileCommandHandler(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<InternalUserModel> Handle(PatchUserProfileCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = await _applicationUserRepository.FindAsync(request.UserId);

            if (applicationUser == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            applicationUser.ChangeProfile(request.Firstname, request.Lastname);

            await _applicationUserRepository.PatchAsync(applicationUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var identityUser = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalUserModel), request.UserId);
            }

            var roles = await _userManager.GetRolesAsync(identityUser);

            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Roles = roles.ToList()
            };
        }
    }
}
