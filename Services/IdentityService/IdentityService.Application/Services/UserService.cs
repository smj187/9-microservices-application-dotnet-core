using IdentityService.Application.Adapters;
using IdentityService.Core.Models;
using IdentityService.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IAuthAdapter _authAdapter;
        private readonly JsonWebTokenSettings _jsonWebTokenSettings;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManger, IOptions<JsonWebTokenSettings> jsonwebtoken, IAuthAdapter authAdapter)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _authAdapter = authAdapter;
            _jsonWebTokenSettings = jsonwebtoken.Value;
        }

        public async Task<Token> AuthenticateAsync(Token token)
        {
            var user = await _userManager.FindByEmailAsync(token.Email);
            if (user == null)
            {
                token.Success = false;
                token.Message = $"No account with {token.Email} registered";
                return token;
            }

            var authenticated = await _userManager.CheckPasswordAsync(user, token.Password);
            if(authenticated == false)
            {
                token.Success = false;
                token.Message = "invalid credentials";
                return token;
            }

            token.Success = true;
            token.JsonWebToken = await _authAdapter.CreateJsonWebToken(user);
            token.Username = user.UserName;
            token.UserId = Guid.Parse(user.Id);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();
            return token;
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
                JsonWebToken = await _authAdapter.CreateJsonWebToken(user),
                Username = user.UserName,
                UserId = Guid.Parse(user.Id),
                Email = user.Email,
            };

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();
            return token;
        }
    }
}
