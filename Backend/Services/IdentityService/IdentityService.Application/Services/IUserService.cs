using IdentityService.Core.Identities;
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
        Task<InternalIdentityUser> RegisterUserAsync(Guid id, string username, string email, string password);
        Task<InternalIdentityUser> LoginUserAsync(string email, string password);


        // token
        //Task<RefreshToken> RenewRefreshTokenAsync(Guid userId, string token);
        //Task RevokeTokenAsync(string token);
        //Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user);
    }
}
