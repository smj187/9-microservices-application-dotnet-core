using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands
{
    public class RefreshAuthenticationCommand : IRequest<Token>
    {
        public string Token { get; set; } = default!;
    }
}
