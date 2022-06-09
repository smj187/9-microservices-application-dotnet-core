using IdentityService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IAdminService
    {
        Task<IReadOnlyCollection<ApplicationUser>> ListUsersAsync();
        Task<ApplicationUser> FindUserAsync(Guid userId);

        Task<ApplicationUser> AddRoleToUser(Guid userId, List<string> roles);
        Task<ApplicationUser> RemoveRoleFromUser(Guid userId, List<string> roles);

        Task<ApplicationUser> LockUserAccountAsync(Guid userId);
        Task<ApplicationUser> UnlockUserAccountAsync(Guid userId);
    }
}
