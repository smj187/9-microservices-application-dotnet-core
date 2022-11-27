using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.Services.Interfaces;
using IdentityService.Core.Identities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<InternalIdentityUser> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly RoleManager<InternalRole> _roleManager;

        public AdminService(UserManager<InternalIdentityUser> userManager, IdentityContext identityContext, RoleManager<InternalRole> roleManager)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _roleManager = roleManager;
        }

        public async Task<InternalIdentityUser> AddRoleToUser(Guid userId, List<string> roles)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalIdentityUser), userId);
            }

            foreach (var role in roles)
            {
                if (!Enum.IsDefined(typeof(Role), role))
                {
                    throw new InvalidUserRoleException($"{role} is not a valid user role");
                }

                var r = await _roleManager.GetRoleNameAsync(new InternalRole(role));
                await _userManager.AddToRoleAsync(identityUser, r);
            }

            return identityUser;
        }

        public async Task<InternalIdentityUser> RemoveRoleFromUser(Guid userId, List<string> roles)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalIdentityUser), userId);
            }

            foreach (var role in roles)
            {
                if (!Enum.IsDefined(typeof(Role), role))
                {
                    throw new InvalidUserRoleException($"{role} is not a valid user role");
                }

                var r = await _roleManager.GetRoleNameAsync(new InternalRole(role));
                await _userManager.RemoveFromRoleAsync(identityUser, r);
            }

            return identityUser;
        }

        public async Task<InternalIdentityUser> FindUserAsync(Guid userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalIdentityUser), userId);
            }

            return identityUser;
        }

        public async Task<InternalIdentityUser> LockUserAccountAsync(Guid userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalIdentityUser), userId);
            }

            await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.UtcNow.AddYears(99));
            await _identityContext.SaveChangesAsync();

            return identityUser;
        }


        public async Task<InternalIdentityUser> UnlockUserAccountAsync(Guid userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId.ToString());
            if (identityUser == null)
            {
                throw new AggregateNotFoundException(nameof(InternalIdentityUser), userId);
            }

            await _userManager.SetLockoutEndDateAsync(identityUser, null);
            await _identityContext.SaveChangesAsync();

            return identityUser;
        }
    }
}
