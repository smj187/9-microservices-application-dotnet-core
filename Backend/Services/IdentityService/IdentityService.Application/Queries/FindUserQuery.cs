using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class FindUserQuery : IRequest<InternalUserModel>
    {
        public required Guid UserId { get; set; }
    }
}
