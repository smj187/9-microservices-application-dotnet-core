using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using IdentityService.Core.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Core.Aggregates
{
    public class ApplicationUser : AggregateBase
    {
        private InternalIdentityUser _internalIdentityUser;

        public Guid InternalUserId { get; set; }

        private string? _firstname = null;
        private string? _lastname = null;
        private string? _avatarUrl = null;

        // ef required (never called)
        public ApplicationUser() 
        {
            _internalIdentityUser = default!;
        }

        public ApplicationUser(Guid id, InternalIdentityUser identityUser, string? firstname = null, string? lastname = null)
        {
            Id = id;
            Guard.Against.Null(identityUser, nameof(identityUser));
            _internalIdentityUser = identityUser;

            _firstname = firstname;
            _lastname = lastname;
            _avatarUrl = null;

            CreatedAt = DateTimeOffset.UtcNow;
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
        }

        public void SetAvatar(string? url = null)
        {
            _avatarUrl = url;
        }
    }
}
