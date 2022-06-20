using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

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
