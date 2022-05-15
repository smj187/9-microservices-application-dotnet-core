using IdentityService.Application.Adapters;
using IdentityService.Core.Entities;
using IdentityService.Core.Models;
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
        private readonly UserManager<User> _userManager;
        private readonly IAuthAdapter _authAdapter;
        private readonly IdentityContext _context;

        public UserService(UserManager<User> userManager, IAuthAdapter authAdapter, IdentityContext context)
        {
            _userManager = userManager;
            _authAdapter = authAdapter;
            _context = context;
        }

        public async Task<Token> RegisterAsync(User user, string password)
        {
            var existing = await _userManager.FindByEmailAsync(user.Email);
            if(existing != null)
            {
                throw new ArgumentException($"a user with {user.Email} already exists");
            }

           
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            if (result.Errors.Any())
            {
                throw new ArgumentException($"failed {result.Errors.Select(e => $"{e.Description},")}");
            }

            var token = new Token
            {
                Success = true,
                Jwt = await _authAdapter.CreateJsonWebToken(user),
                Username = user.UserName,
                UserId = Guid.Parse(user.Id),
                Email = user.Email,
            };

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();
            return token;
        }


        public async Task<User> FindUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString());
            if (user == null)
            {
                throw new ArgumentNullException($"no user with id {userId}");
            }

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            user.Roles = roles.ToList();
            return user;
        }

        public async Task<IReadOnlyCollection<User>> ListUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                user.Roles = roles.ToList();
            }

            return users;
        }

        public async Task<bool> TerminateUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString());
            if (user == null)
            {
                throw new ArgumentNullException($"no user with id {userId}");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }



        public async Task<User> PromoteRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"{userId} does not exist");
            }

            var store = new RoleStore<IdentityRole>(_context);
            var allRoles = store.Roles.ToList();

            if (!allRoles.Select(r => r.Name).Contains(role))
            {
                throw new Exception($"{role} is not a valid role");
            }

            await _userManager.AddToRoleAsync(user, role);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            user.Roles = roles.ToList();

            return user;
        }

        public async Task<User> RevokeRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"{userId} does not exist");
            }

            var store = new RoleStore<IdentityRole>(_context);
            var allRoles = store.Roles.ToList();

            if (!allRoles.Select(r => r.Name).Contains(role))
            {
                throw new Exception($"{role} is not a valid role");
            }

            await _userManager.RemoveFromRoleAsync(user, role);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            user.Roles = roles.ToList();

            return user;
        }

    }
}
