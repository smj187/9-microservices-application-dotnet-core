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

        Task<ApplicationUser> UpdateUserProfile(Guid userId, string? firstname, string? lastname);
   
        Task<ApplicationUser> FindProfile(Guid userId);



        // token
        Task<RefreshToken> RenewRefreshToken(Guid userId, string token);
        Task RevokeToken(string token);
        
        Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user);



    }
}
