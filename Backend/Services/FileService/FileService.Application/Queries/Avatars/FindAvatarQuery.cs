using FileService.Core.Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Queries.Users
{
    public class FindAvatarQuery : IRequest<Avatar>
    {
        public Guid UserId { get; set; }
    }
}
