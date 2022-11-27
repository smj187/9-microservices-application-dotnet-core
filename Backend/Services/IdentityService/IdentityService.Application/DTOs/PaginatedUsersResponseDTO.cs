using BuildingBlocks.EfCore.Helpers;
using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.DTOs
{
    public class PaginatedUsersResponseDTO
    {
        public List<InternalUserModel> Users { get; set; } = new();
        public PagedResultBase Pagination { get; set; } = new();
    }
}
