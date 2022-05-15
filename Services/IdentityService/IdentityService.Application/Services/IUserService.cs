using IdentityService.Core.Entities;
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
        
        Task<User> FindUserAsync(Guid userId);
        Task<IReadOnlyCollection<User>> ListUsersAsync();
        Task<bool> TerminateUserAsync(Guid userId);

        Task<User> PromoteRoleAsync(Guid userId, string role);
        Task<User> RevokeRoleAsync(Guid userId, string role);
    }
}
