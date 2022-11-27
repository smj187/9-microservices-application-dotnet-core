using IdentityService.Core.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJsonWebToken(string email);
        RefreshToken CreateRefreshToken();
        Task<bool> ValidateJsonWebToken(string token, string? requiredRoles = null);
    }
}
