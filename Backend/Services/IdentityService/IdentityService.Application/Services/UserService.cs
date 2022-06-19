using BuildingBlocks.Exceptions;
using BuildingBlocks.Exceptions.Authentication;
using BuildingBlocks.Exceptions.Domain;
using IdentityService.Core.Entities;
using IdentityService.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

     

       

        public async Task<ApplicationUser> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AggregateNotFoundException($"the address '{email}' is not registerd");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);
            if (result.IsLockedOut)
            {
                throw new AuthenticationException("this account is banned");
            }

            if (!result.Succeeded)
            {
                throw new AuthenticationException("email or password is not correct");
            }

            

            return await AssignRolesToUserAsync(user);
        }

        public async Task<RefreshToken> RenewRefreshTokenAsync(Guid userId, string token)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no user with this refresh token exists");
            }

            var existingToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!existingToken.IsActive)
            {
                throw new AuthenticationException("token is not active");
            }

            existingToken.RevokedAt = DateTimeOffset.UtcNow;

            var newToken = CreateRefreshToken();
            user.AddToken(newToken);
            _context.Update(user);
            

            // remove all expired tokens
            var expiredTokens = user.RefreshTokens.Where(x => x.IsExpired);
            foreach (var expiredToken in expiredTokens)
            {
                user.RemoveToken(expiredToken);
            }

            await _context.SaveChangesAsync();
            return newToken;
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception();
            }

            if (user.RefreshTokens.Any(x => x.IsActive))
            {
                var active = user.RefreshTokens.FirstOrDefault(x => x.IsActive == true);
                if (active != null)
                {
                    return active;
                }
            }

            var token = CreateRefreshToken();
            user.AddToken(token);
            _context.Update(user);
            await _context.SaveChangesAsync();
            return token;
        }

        private static RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(10),
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        

        
        public async Task<ApplicationUser> RegisterUserAsync(string username, string email, string password, string? firstname, string? lastname)
        {
            var existing = await _userManager.FindByEmailAsync(email);
            if (existing != null)
            {
                throw new DomainViolationException($"the address '{email}' is not available");
            }

            var user = new ApplicationUser(firstname, lastname)
            {
                UserName = username,
                Email = email
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

        public async Task RevokeTokenAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                throw new AggregateNotFoundException($"no user with this refresh token exists");
            }

            var rToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!rToken.IsActive)
            {
                throw new AuthenticationException("token is not active");
            }

            rToken.RevokedAt = DateTimeOffset.UtcNow;

            _context.Update(user);
            _context.SaveChanges();

        }

        public async Task<ApplicationUser> UpdateUserProfileAsync(Guid userId, string? firstname, string? lastname)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            user.ChangeProfile(firstname, lastname);

            _context.Update(user);
            _context.SaveChanges();


            return await AssignRolesToUserAsync(user);
        }

        public async Task<ApplicationUser> FindProfileAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            return await AssignRolesToUserAsync(user);
        }

        public async Task AddAvatarToProfileAsync(Guid userId, string url)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new AggregateNotFoundException($"no such user with id'{userId}'");
            }

            user.SetAvatar(url);
        }
    }
}
