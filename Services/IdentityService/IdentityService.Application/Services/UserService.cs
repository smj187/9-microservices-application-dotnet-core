using IdentityService.Application.Adapters;
using IdentityService.Core.Models;
using Microsoft.AspNetCore.Identity;
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

        public UserService(UserManager<User> userManager, IAuthAdapter authAdapter)
        {
            _userManager = userManager;
            _authAdapter = authAdapter;
        }

        public async Task<Token> AuthenticateAsync(Token token)
        {
            var user = await _userManager.FindByEmailAsync(token.Email);
            if (user == null)
            {
                token.Success = false;
                token.Message = $"{token.Email} is not registered";
                return token;
            }

            var authenticated = await _userManager.CheckPasswordAsync(user, token.Password);
            if(authenticated == false)
            {
                token.Success = false;
                token.Message = "Invalid credentials";
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
