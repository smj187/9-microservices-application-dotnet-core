using IdentityService.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IUserService
    {
        Task<ApplicationUser> RegisterUserAsync(string username, string email, string password, string? firstname, string? lastname);
        Task<ApplicationUser> LoginUserAsync(string email, string password);

        Task<ApplicationUser> AddRoleToUser(Guid userId, List<string> roles);
        Task<ApplicationUser> RemoveRoleFromUser(Guid userId, List<string> roles);

        Task<ApplicationUser> FindUserAsync(Guid userId);
        Task<IReadOnlyCollection<ApplicationUser>> ListUsersAsync();
    }
}
