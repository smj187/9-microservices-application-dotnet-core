using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using IdentityService.Core.Identities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Aggregates
{
    public class ApplicationUser : AggregateBase, IMultitenantAggregate
    {
        private InternalIdentityUser _internalIdentityUser;

        public Guid InternalUserId { get; set; }

        private string? _firstname = null;
        private string? _lastname = null;
        private string? _avatarUrl = null;
        private string _tenantId;
        private List<RefreshToken> _refreshTokens = new();

        // ef required (never called)
        public ApplicationUser()
        {
            _internalIdentityUser = default!;
            _tenantId = default!;
            _refreshTokens = default!;
        }

        public ApplicationUser(string tenantId, Guid id, InternalIdentityUser identityUser, string? firstname = null, string? lastname = null)
        {
            Id = id;
            Guard.Against.Null(identityUser, nameof(identityUser));
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            _internalIdentityUser = identityUser;

            _tenantId = tenantId;
            _firstname = firstname;
            _lastname = lastname;
            _avatarUrl = null;

            CreatedAt = DateTimeOffset.UtcNow;
            _refreshTokens = new();
        }

        public List<RefreshToken> RefreshTokens
        {
            get => _refreshTokens;
            private set => _refreshTokens = value;
        }

        public RefreshToken? GetActiveRefreshToken()
        {
            var active = _refreshTokens.Where(r => r.IsActive).FirstOrDefault();
            if (active != null)
            {
                return active;
            }

            return null;
        }

        public RefreshToken? RevokeRefreshToken(string token)
        {
            var refreshToken = _refreshTokens.FirstOrDefault(r => r.Token == token);
            if (refreshToken == null)
            {
                return null;
            }

            refreshToken.RevokedAt = DateTimeOffset.UtcNow;

            return refreshToken;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            Guard.Against.Null(refreshToken, nameof(refreshToken));

            _refreshTokens.Add(refreshToken);

            Modify();
        }


        public string TenantId
        {
            get => _tenantId;
            set => _tenantId = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        public InternalIdentityUser InternalIdentityUser
        {
            get => _internalIdentityUser;
            private set => _internalIdentityUser = value;
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


        public void ChangeProfile(string? firstname = null, string? lastname = null)
        {
            _firstname = firstname;
            _lastname = lastname;

            Modify();
        }

        public void SetAvatar(string? url = null)
        {
            _avatarUrl = url;

            Modify();
        }
    }
}
