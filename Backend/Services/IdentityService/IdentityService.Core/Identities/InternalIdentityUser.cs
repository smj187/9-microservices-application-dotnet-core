using Ardalis.GuardClauses;
using IdentityService.Core.Aggregates;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Identities
{
    public class InternalIdentityUser : IdentityUser<Guid>
    {
        public ApplicationUser AppUser { get; set; }

        protected InternalIdentityUser() { }

        public InternalIdentityUser(Guid id, string email, string username)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(username, nameof(username));

            Id = id;
            Email = email;
            UserName = username;
        }

    }
}
