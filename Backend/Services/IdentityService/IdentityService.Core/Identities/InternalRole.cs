using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IdentityService.Core.Identities
{
    public class InternalRole : IdentityRole<Guid>
    {
        protected internal InternalRole() { }

        public InternalRole(string role)
        {
            Guard.Against.NullOrWhiteSpace(role, nameof(role));
            Name = role;
        }
    }
}
