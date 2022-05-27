﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IAuthService
    {
        Task<string> CreateJsonWebToken(string email);
    }
}
