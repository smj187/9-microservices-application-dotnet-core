using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Adapters
{
    public interface IAuthAdapter
    {
        Task<string> CreateJsonWebToken(User user);

        RefreshToken CreateRefreshToken();
    }
}
