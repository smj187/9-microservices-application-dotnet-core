using AutoMapper;
using IdentityService.Application.DTOs;
using IdentityService.Contracts.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.API.Profiles
{
    public class AddPaginatedProductsMappingAction : IMappingAction<PaginatedUsersResponseDTO, PaginatedUserResponse>
    {
        public void Process(PaginatedUsersResponseDTO source, PaginatedUserResponse destination, ResolutionContext context)
        {
            destination.Pagination = context.Mapper.Map<PaginationResponse>(source.Pagination);
            destination.Users = context.Mapper.Map<IReadOnlyCollection<AdminUserResponse>>(source.Users);
        }
    }
}