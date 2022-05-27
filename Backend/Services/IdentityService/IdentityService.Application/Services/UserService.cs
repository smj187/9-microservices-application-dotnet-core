using IdentityService.Core.Entities;
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

        public UserService(UserManager<ApplicationUser> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApplicationUser> FindUserAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> ListUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByIdAsync(email);
            if (user == null)
            {
                throw new ArgumentNullException($"the address '{email}' is not registerd");
            }

            return user;
        }

        public async Task<ApplicationUser> RegisterUserAsync(string username, string email, string password)
        {
            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null)
            {
                throw new ArgumentException($"the address '{email}' is not available");
            }

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            if (result.Errors.Any())
            {
                throw new ArgumentException($"failed {result.Errors.Select(e => $"{e.Description},")}");
            }

            return user;
        }
    }
}
