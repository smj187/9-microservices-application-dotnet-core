using IdentityService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries.Users
{
    public class ListUsersQuery : IRequest<IReadOnlyCollection<ApplicationUser>>
    {

    }
}
