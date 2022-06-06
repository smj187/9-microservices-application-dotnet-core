using IdentityService.Application.Queries.Admins;
using IdentityService.Application.Services;
using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers.Admin
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, IReadOnlyCollection<ApplicationUser>>
    {
        private readonly IAdminService _adminService;

        public ListUsersQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IReadOnlyCollection<ApplicationUser>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.ListUsersAsync();
        }
    }
}
