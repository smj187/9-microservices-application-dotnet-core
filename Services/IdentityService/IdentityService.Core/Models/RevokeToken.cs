using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Models
{
    public class RevokeToken
    {
        public string JsonWebToken { get; set; } = default!;
    }
}
