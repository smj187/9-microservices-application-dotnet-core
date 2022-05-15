using IdentityService.Application.Adapters;
using IdentityService.Core.Entities;
using IdentityService.Core.Models;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthAdapter _authAdapter;
        private readonly IdentityContext _context;

        public IdentityService(UserManager<User> userManager, IAuthAdapter authAdapter, IdentityContext context)
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
            if (authenticated == false)
            {
                token.Success = false;
                token.Message = "invalid credentials";
                return token;
            }

            token.Success = true;
            token.Jwt = await _authAdapter.CreateJsonWebToken(user);
            token.Username = user.UserName;
            token.UserId = Guid.Parse(user.Id);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();

            if (user.RefreshTokens.Any(r => r.IsActive))
            {
                var active = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                token.RefreshToken = active!.Token;
                token.RefreshTokenExpiration = active.ExpiresAt;
            }
            else
            {
                var refresh = _authAdapter.CreateRefreshToken();
                token.RefreshToken = refresh.Token;
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



        public async Task<Token> RefreshAuthenticationAsync(string refreshToken)
        {
            var token = new Token();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
            if (user == null)
            {
                token.Success = false;
                token.Message = "no valid token found";
                return token;
            }

            var refresh = user.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
            if (refresh == null || !refresh.IsActive)
            {
                token.Success = false;
                token.Message = "token is not active";
                return token;
            }

            // mark as revoked
            refresh.RevokedAt = DateTimeOffset.Now;

            // generate new refresh token
            var newRefresh = _authAdapter.CreateRefreshToken();
            user.RefreshTokens.Add(newRefresh);
            _context.Update(user);
            await _context.SaveChangesAsync();

            token.Success = true;
            token.Jwt = await _authAdapter.CreateJsonWebToken(user);
            token.Username = user.UserName;
            token.UserId = Guid.Parse(user.Id);

            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            token.Roles = roles.ToList();

            token.RefreshToken = newRefresh.Token;
            token.RefreshTokenExpiration = newRefresh.ExpiresAt;

            return token;
        }

        

        public async Task<bool> RevokeRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
            if (user == null)
            {
                return false;
            }

            var refresh = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);
            if (refresh == null || !refresh.IsActive)
            {
                return false;
            }

            // revoke token
            refresh.RevokedAt = DateTimeOffset.Now;
            _context.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
