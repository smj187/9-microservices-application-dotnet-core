using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IAdminService
    {
        Task<InternalIdentityUser> AddRoleToUser(Guid userId, List<string> roles);
        Task<InternalIdentityUser> RemoveRoleFromUser(Guid userId, List<string> roles);

        Task<InternalIdentityUser> LockUserAccountAsync(Guid userId);
        Task<InternalIdentityUser> UnlockUserAccountAsync(Guid userId);
    }
}
