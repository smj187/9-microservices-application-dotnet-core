﻿using IdentityService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Commands.Users
{
    public class PatchUserProfileCommand : IRequest<ApplicationUser>
    {
        public Guid UserId { get; set; }
        public string? Firstname { get; set; } = default!;
        public string? Lastname { get; set; } = default!;
    }
}
