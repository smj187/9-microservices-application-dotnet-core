using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<ApplicationUser> userManager, IJwtService jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<string> CreateJsonWebToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaimsAsync(claims, user);
            var token = await GenerateTokenAsync(identityClaims);

            return token;
        }

        private async Task<ClaimsIdentity> GetUserClaimsAsync(ICollection<Claim> claims, ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTimeOffset.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTimeOffset.UtcNow).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("uid", user.Id));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("roles", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private async Task<string> GenerateTokenAsync(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var audience = _configuration.GetValue<string>("JsonWebToken:Audience");
            var expires = _configuration.GetValue<string>("JsonWebToken:DurationInMinutes");
            var issuer = _configuration.GetValue<string>("JsonWebToken:Issuer");

            var key = await _jwtService.GetCurrentSigningCredentials();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(expires)),
                SigningCredentials = key,
                Audience = audience,
            });

            return tokenHandler.WriteToken(token);
        }


        private static long ToUnixEpochDate(DateTimeOffset date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);


    }
}
