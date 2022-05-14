using IdentityService.Core.Models;
using IdentityService.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly JsonWebTokenSettings _jsonwebtoken;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManger, IOptions<JsonWebTokenSettings> jsonwebtoken)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _jsonwebtoken = jsonwebtoken.Value;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            var existing = await _userManager.FindByEmailAsync(user.Email);
            if(existing != null)
            {
                throw new ArgumentException($"a user with {user.Email} does already exist");
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

            return user;
        }
    }
}
