using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Queries
{
    public class FindUserQuery : IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
