using IdentityService.Core.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface ITokenService
    {
        Task<string> CreateJsonWebToken(string email);
        RefreshToken CreateRefreshToken();
    }
}
