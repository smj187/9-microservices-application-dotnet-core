using IdentityService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IUserService
    {
        // user
        Task<ApplicationUser> RegisterUserAsync(string username, string email, string password, string? firstname, string? lastname);
        Task<ApplicationUser> LoginUserAsync(string email, string password);

        Task<ApplicationUser> UpdateUserProfileAsync(Guid userId, string? firstname, string? lastname);
        Task AddAvatarToProfileAsync(Guid userId, string url);   
        Task<ApplicationUser> FindProfileAsync(Guid userId);



        // token
        Task<RefreshToken> RenewRefreshTokenAsync(Guid userId, string token);
        Task RevokeTokenAsync(string token);
        
        Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user);



    }
}
