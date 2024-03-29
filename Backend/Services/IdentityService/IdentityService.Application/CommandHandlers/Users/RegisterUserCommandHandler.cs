﻿using BuildingBlocks.EfCore.Repositories.Interfaces;
using IdentityService.Application.Commands.Users;
using IdentityService.Application.Services;
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

namespace IdentityService.Application.CommandHandlers.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, InternalUserModel>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<InternalIdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public RegisterUserCommandHandler(IUserService userService, ITokenService authService, UserManager<InternalIdentityUser> userManager, IUnitOfWork unitOfWork, IApplicationUserRepository applicationUserRepository)
        {
            _userService = userService;
            _tokenService = authService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _applicationUserRepository = applicationUserRepository;
        }

        public async Task<InternalUserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUserId = Guid.NewGuid();

            var identityUser = await _userService.RegisterUserAsync(newUserId, request.Username, request.Email, request.Password);

            var applicationUser = await _applicationUserRepository.AddAsync(new ApplicationUser(request.TenantId, newUserId, identityUser, request.Firstname, request.Lastname));

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            var token = await _tokenService.CreateJsonWebToken(request.Email);

            //var refresh = await _userService.CreateRefreshTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(identityUser);

            return new InternalUserModel
            {
                ApplicationUser = applicationUser,
                InternalIdentityUser = identityUser,
                Roles = roles.ToList(),
                Token = token
            };
        }
    }
}
