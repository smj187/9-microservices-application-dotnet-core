using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1.Contracts
{
    // requests
    public record UserRegisterRequest([Required] string Username, [Required] string Email, [Required] string EmailConfirm, [Required] string Password, [Required] string PasswordConfirm, string? Firstname, string? Lastname);
    public record UserLoginRequest([Required] string Email, [Required] string Password);

    public record RefreshTokenRequest(string Token);
    public record RevokeTokenRequest(string Token);
    public record PatchUserProfileRequest(string? Firstname, string? Lastname);


    public record AddRoleToUserRequest([Required] List<string> Roles);
    public record RemoveRoleFromUserRequest([Required] List<string> Roles);




    public record UserResponse
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public List<string> Roles { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }
    }

    public class UserProfileResponse
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public List<string> Roles { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }

    public class AdminUserResponse
    {
        public Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public List<string> Roles { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }



    // responses


    public record AdminRefreshTokenResponse(string Token, DateTimeOffset ExpiresAt, bool IsExpired, DateTimeOffset CreatedAt, DateTimeOffset RevokedAt, bool IsActive);

}
