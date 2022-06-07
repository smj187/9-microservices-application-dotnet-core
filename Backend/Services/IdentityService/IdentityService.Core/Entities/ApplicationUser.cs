using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        private List<string> _roles = new();

        private string? _firstname = null;
        private string? _lastname = null;
        private string? _avatarUrl = null;
        private HashSet<RefreshToken> _refreshTokens = new();
        private DateTimeOffset _createdAt;
        private DateTimeOffset? _modifiedAt;

        public ApplicationUser(string? firstname = null, string? lastname = null)
        {
            _firstname = firstname;
            _lastname = lastname;
            _avatarUrl = null;

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
        }



        public string? Firstname
        {
            get => _firstname;
            private set => _firstname = value;
        }

        public string? Lastname
        {
            get => _lastname;
            private set => _lastname = value;
        }

        public string? AvatarUrl
        {
            get => _avatarUrl;
            private set => _avatarUrl = value;
        }

        public DateTimeOffset CreatedAt
        {
            get => _createdAt;
            private set => _createdAt = value;
        }
        public DateTimeOffset? ModifiedAt
        {
            get => _modifiedAt;
            private set => _modifiedAt = value;
        }


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


        public IEnumerable<RefreshToken> RefreshTokens
        {
            get => _refreshTokens;
            private set => _refreshTokens = new HashSet<RefreshToken>(value);
        }


        public void AddToken(RefreshToken token)
        {
            Guard.Against.Null(token, nameof(token));

            _refreshTokens.Add(token);
        }

        public void RemoveToken(RefreshToken token)
        {
            Guard.Against.Null(token, nameof(token));

            _refreshTokens.Remove(token);
        }


        public void ChangeProfile(string? firstname = null, string? lastname = null)
        {
            _firstname = firstname;
            _lastname = lastname;
        }

        public void SetAvatar(string? url)
        {
            _avatarUrl = url;
        }
    }
}
