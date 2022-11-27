using IdentityService.Application.DTOs;
using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class ListUsersQuery : IRequest<PaginatedUsersResponseDTO>
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
