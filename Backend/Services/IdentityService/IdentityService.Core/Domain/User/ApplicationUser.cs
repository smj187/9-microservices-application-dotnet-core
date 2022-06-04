using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Interfaces;
using IdentityService.Core.Domain.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Domain.User
{
    public class ApplicationUser : IdentityUser
    {
        private List<string> _roles = new();

        public string? Firstname { get; set; } = null;
        public string? Lastname { get; set; } = null;

        public string? AvatarUrl { get; set; } = null;

        // favourite products
        // purchase history
        // refresh tokens

        [NotMapped]
        public List<string> Roles
        {
            get => _roles;
            private set => _roles = value;
        }

        public void SetRoles(List<string> roles)
        {
            Guard.Against.Null(roles, nameof(roles));

            _roles = roles;
        }

    }
}
