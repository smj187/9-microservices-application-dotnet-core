using IdentityService.Core.Entities;
using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IIdentityService
    {
        Task<Token> AuthenticateAsync(Token token);

        Task<Token> RefreshAuthenticationAsync(string refreshToken);

        Task<bool> RevokeRefreshTokenAsync(Guid userId, string refreshToken);

        Task<IReadOnlyCollection<RefreshToken>> ListUserTokensAsync(Guid userId);
    }
}
