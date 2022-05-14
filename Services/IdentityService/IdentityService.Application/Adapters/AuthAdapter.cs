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

namespace IdentityService.Application.Adapters
{
    public class AuthAdapter : IAuthAdapter
    {
        private readonly UserManager<User> _userManager;
        private readonly JsonWebTokenSettings _jsonWebTokenSettings;

        public AuthAdapter(UserManager<User> userManager, IOptions<JsonWebTokenSettings> jsonWebTokenSettings)
        {
            _userManager = userManager;
            _jsonWebTokenSettings = jsonWebTokenSettings.Value;
        }


        public async Task<string> CreateJsonWebToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(user);

            var token = GenerateToken(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jsonWebTokenSettings.Issuer,
                audience: _jsonWebTokenSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jsonWebTokenSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var securet = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jsonWebTokenSettings.Key));
            return new SigningCredentials(securet, SecurityAlgorithms.HmacSha256); ;
        }

        private async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new System.Security.Cryptography.RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                JsonWebToken = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow
            };
        }

    }
}
