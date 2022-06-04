using BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Domain.Admin
{
    public enum Role
    {
        Administrator = 0,
        Moderator = 1,
        User = 2
    }

}
