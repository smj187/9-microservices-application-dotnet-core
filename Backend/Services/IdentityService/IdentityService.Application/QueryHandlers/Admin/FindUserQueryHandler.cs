using BuildingBlocks.Exceptions.Domain;
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
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, ApplicationUser>
    {
        private readonly IAdminService _adminService;

        public FindUserQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<ApplicationUser> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _adminService.FindUserAsync(request.UserId);
            if (user == null)
            {
                throw new AggregateNotFoundException(nameof(ApplicationUser), request.UserId);
            }

            return user;
        }
    }
}
