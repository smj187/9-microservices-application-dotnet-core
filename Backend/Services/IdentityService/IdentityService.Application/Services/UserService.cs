using BuildingBlocks.Exceptions;
using IdentityService.Core.Domain.Admin;
using IdentityService.Core.Domain.User;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, IdentityContext context, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        private async Task<ApplicationUser> AssignRolesToUserAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            user.SetRoles(roles.ToList());
            return user;
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
            var users = await _userManager.Users.ToListAsync();

            foreach(var user in users)
            {
                await AssignRolesToUserAsync(user);
            }

            return users;
        }

        public async Task<ApplicationUser> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AggregateNotFoundException($"the address '{email}' is not registerd");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            if (!result.Succeeded)
            {
                throw new AuthenticationException("email or password is not correct");
            }

            return await AssignRolesToUserAsync(user);
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

        public async Task<ApplicationUser> RegisterUserAsync(string username, string email, string password, string? firstname, string? lastname)
        {
            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null)
            {
                throw new DomainViolationException($"the address '{email}' is not available");
            }

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                Firstname = firstname,
                Lastname = lastname
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.User.ToString());
            }

            if (result.Errors.Any())
            {
                throw new DomainViolationException($"failed to create new user {result.Errors.Select(e => $"{e.Description},")}");
            }

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
    }
}
