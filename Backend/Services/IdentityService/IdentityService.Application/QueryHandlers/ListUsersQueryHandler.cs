using BuildingBlocks.Exceptions.Domain;
using IdentityService.Application.DTOs;
using IdentityService.Application.Queries;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.QueryHandlers
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, PaginatedUsersResponseDTO>
    {
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<InternalIdentityUser> _userManager;

        public ListUsersQueryHandler(IApplicationUserRepository applicationUserRepository, UserManager<InternalIdentityUser> userManager)
        {
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }

        public async Task<PaginatedUsersResponseDTO> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var paginatedUsers = await _applicationUserRepository.ListAsync(request.Page, request.PageSize);

            var userIds = paginatedUsers.Results.Select(u => u.Id).ToList();
            var applicationUsers = await _applicationUserRepository.ListAsync(userIds);

            var response = new PaginatedUsersResponseDTO();

            foreach (var applicationUser in applicationUsers)
            {
                var identityUser = await _userManager.FindByIdAsync(applicationUser.InternalUserId.ToString());
                if (identityUser == null)
                {
                    throw new AggregateNotFoundException(nameof(applicationUser), applicationUser.InternalUserId);
                }
                var roles = await _userManager.GetRolesAsync(identityUser);

                response.Users.Add(new InternalUserModel
                {
                    ApplicationUser = applicationUser,
                    InternalIdentityUser = identityUser,
                    Roles = roles.ToList()
                });
            }

            response.Pagination.CurrentPage = paginatedUsers.CurrentPage;
            response.Pagination.PageCount = paginatedUsers.PageCount;
            response.Pagination.PageSize = paginatedUsers.PageSize;
            response.Pagination.RowCount = paginatedUsers.RowCount;
  
            return response;
        }
    }
}