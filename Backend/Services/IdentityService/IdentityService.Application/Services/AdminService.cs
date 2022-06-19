using BuildingBlocks.Exceptions.Domain;
using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IdentityContext _identityContext;

        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IdentityContext identityContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityContext = identityContext;
        }

        private async Task<ApplicationUser> AssignRolesToUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            user.SetRoles(roles.ToList());
            return user;
        }


        public async Task<ApplicationUser> AddRoleToUser(Guid userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }


            foreach (var role in roles)
            {
                if (!Enum.IsDefined(typeof(Role), role))
                {
                    throw new DomainViolationException($"{role} is not a valid user role");
                }

                var r = await _roleManager.GetRoleNameAsync(new IdentityRole(role));
                await _userManager.AddToRoleAsync(user, r);
            }

            return await AssignRolesToUserAsync(user);
        }

        public async Task<ApplicationUser> FindUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no user with '{user}' registerd");
            }

            return await AssignRolesToUserAsync(user);
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ListUsersAsync()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                await AssignRolesToUserAsync(user);
            }

            return users;
        }

        public async Task<ApplicationUser> LockUserAccountAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddYears(99));
            await _identityContext.SaveChangesAsync();

            return await AssignRolesToUserAsync(user);
        }

        public async Task<ApplicationUser> RemoveRoleFromUser(Guid userId, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            foreach (var role in roles)
            {
                if (!Enum.IsDefined(typeof(Role), role))
                {
                    throw new DomainViolationException($"{role} is not a valid user role");
                }

                var r = await _roleManager.GetRoleNameAsync(new IdentityRole(role));
                await _userManager.RemoveFromRoleAsync(user, r);
            }

            return await AssignRolesToUserAsync(user);
        }

        public async Task<ApplicationUser> UnlockUserAccountAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            await _userManager.SetLockoutEndDateAsync(user, null);
            await _identityContext.SaveChangesAsync();

            return await AssignRolesToUserAsync(user);
        }
    }
}
