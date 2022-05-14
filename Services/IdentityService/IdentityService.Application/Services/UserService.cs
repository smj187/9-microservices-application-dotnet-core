using IdentityService.Application.Adapters;
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

            if (user.RefreshTokens.Any(r => r.IsActive))
            {
                var active = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                token.RefreshToken = active.JsonWebToken;
                token.RefreshTokenExpiration = active.ExpiresAt;
            }
            else
            {
                var refresh = _authAdapter.CreateRefreshToken();
                token.RefreshToken = refresh.JsonWebToken;
                token.RefreshTokenExpiration = refresh.ExpiresAt;

                user.RefreshTokens.Add(refresh);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }


            return token;
        }

        public async Task<IReadOnlyCollection<RefreshToken>> ListUserTokensAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new ArgumentException($"{userId} does not exist");
            }

            return user.RefreshTokens;
        }

        public async Task<User> PromoteRoleAsync(string username, string newRole)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"{username} does not exist");
            }

            var store = new RoleStore<IdentityRole>(_context);
            var allRoles = store.Roles.ToList();

            if (!allRoles.Select(r => r.Name).Contains(newRole))
            {
                throw new Exception($"{newRole} is not a valid role");
            }

            await _userManager.AddToRoleAsync(user, newRole);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            user.Roles = roles.ToList();

            return user;
        }

        public async Task<Token> RefreshTokenAsync(string jsonWebToken)
        {
            var token = new Token();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.JsonWebToken == jsonWebToken));
            if (user == null)
            {
                token.Success = false;
                token.Message = "no valid token found";
                return token;
            }

            var refresh = user.RefreshTokens.FirstOrDefault(t => t.JsonWebToken == jsonWebToken);
            if (refresh == null || !refresh.IsActive)
            {
                token.Success = false;
                token.Message = "token is not active";
                return token;
            }

            // revoke
            refresh.RevokedAt = DateTimeOffset.Now;

            // generate new refresh token
            var newRefresh = _authAdapter.CreateRefreshToken();
            user.RefreshTokens.Add(newRefresh);
            _context.Update(user);
            await _context.SaveChangesAsync();

            token.Success = true;
            token.JsonWebToken = await _authAdapter.CreateJsonWebToken(user);
            token.Username = user.UserName;
            token.UserId = Guid.Parse(user.Id);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();

            token.RefreshToken = newRefresh.JsonWebToken;
            token.RefreshTokenExpiration = newRefresh.ExpiresAt;

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

        public async Task<bool> RevokeRefreshTokenAsync(Guid userId, string jsonWebToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
            if (user == null)
            {
                return false;
            }

            var refresh = user.RefreshTokens.FirstOrDefault(r => r.JsonWebToken == jsonWebToken);
            if(refresh == null || !refresh.IsActive)
            {
                return false;
            }

            // revoke token
            refresh.RevokedAt = DateTimeOffset.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User> RevokeRoleAsync(string username, string roleToRemove)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new KeyNotFoundException($"{username} does not exist");
            }

            var store = new RoleStore<IdentityRole>(_context);
            var allRoles = store.Roles.ToList();

            if (!allRoles.Select(r => r.Name).Contains(roleToRemove))
            {
                throw new Exception($"{roleToRemove} is not a valid role");
            }

            await _userManager.RemoveFromRoleAsync(user, roleToRemove);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            user.Roles = roles.ToList();

            return user;
        }
    }
}
