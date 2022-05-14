using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IUserService
    {
        Task<Token> RegisterAsync(User user, string password);
        Task<Token> AuthenticateAsync(Token token);

        Task<User> PromoteRoleAsync(string username, string newRole);
        Task<User> RevokeRoleAsync(string username, string roleToRemove);

        Task<Token> RefreshTokenAsync(string jsonWebToken);

        // TODO: invalidate currently active token

        Task<bool> RevokeRefreshTokenAsync(Guid userId, string jsonWebToken);

        Task<IReadOnlyCollection<RefreshToken>> ListUserTokensAsync(Guid userId);
    }
}
