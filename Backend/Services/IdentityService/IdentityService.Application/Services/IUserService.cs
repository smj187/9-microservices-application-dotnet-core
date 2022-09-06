using IdentityService.Core.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IUserService
    {
        Task<InternalIdentityUser> RegisterUserAsync(Guid id, string username, string email, string password);
        Task<InternalIdentityUser> LoginUserAsync(string email, string password);
        Task<InternalIdentityUser> FindIdentityUser(Guid id);
    }
}
