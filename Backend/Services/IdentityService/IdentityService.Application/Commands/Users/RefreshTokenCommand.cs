﻿using IdentityService.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Users
{
    public class RefreshTokenCommand : IRequest<InternalUserModel>
    {
        public Guid UserId { get; set; }

        public string Token { get; set; } = default!;
    }
}
