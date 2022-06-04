using IdentityService.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Models
{
    public record AuthenticatedUser(ApplicationUser User, string Token);
}
