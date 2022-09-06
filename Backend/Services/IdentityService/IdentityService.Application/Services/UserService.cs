using BuildingBlocks.Exceptions;
using BuildingBlocks.Exceptions.Authentication;
using BuildingBlocks.Exceptions.Domain;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<InternalIdentityUser> _userManager;
        private readonly SignInManager<InternalIdentityUser> _signInManager;

        public UserService(UserManager<InternalIdentityUser> userManager, SignInManager<InternalIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<InternalIdentityUser> FindIdentityUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<InternalIdentityUser> LoginUserAsync(string email, string password)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            if (identityUser == null)
            {
                throw new AggregateNotFoundException($"the address '{email}' is not registerd");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(identityUser, password, true);
            if (result.IsLockedOut)
            {
                throw new AuthenticationException("this account is banned");
            }

            if (!result.Succeeded)
            {
                throw new AuthenticationException("email or password is not correct");
            }

            return identityUser;
        }

        public async Task<InternalIdentityUser> RegisterUserAsync(Guid id, string username, string email, string password)
        {
            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null)
            {
                throw new DomainViolationException($"the address '{email}' is not available");
            }

            var identityUser = new InternalIdentityUser(id, email, username);
            var result = await _userManager.CreateAsync(identityUser, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, Role.User.ToString());
            }

            if (result.Errors.Any())
            {
                throw new DomainViolationException($"failed to create new user {result.Errors.Select(e => $"{e.Description},")}");
            }

            return identityUser;
        }
    }
}
