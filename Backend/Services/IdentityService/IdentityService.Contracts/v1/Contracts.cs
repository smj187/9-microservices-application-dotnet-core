using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1
{

    #region requests
    public class UserRegisterRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string EmailConfirm { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }


    public class UserLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class RefreshTokenRequest
    {
        public required string Token { get; set; }
    }

    public class RevokeTokenRequest
    {
        public required string Token { get; set; }
    }


    public class PatchUserProfileRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }


    public class AddRoleToUserRequest
    {
        public required List<string> Roles { get; set; }
    }

    public class RemoveRoleFromUserRequest
    {
        public required List<string> Roles { get; set; }
    }
    #endregion



    #region responses
    public class UserResponse
    {
        public required Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public required List<string> Roles { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }

    public class UserProfileResponse
    {
        public required Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public required List<string> Roles { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }

    public class AdminUserResponse
    {
        public required Guid Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? AvatarUrl { get; set; }

        public required List<string> Roles { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }

        public required DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
    }

    public class TokenResponse
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
    }


    // paginations
    public class PaginationResponse
    {
        public required int CurrentPage { get; set; }
        public required int TotalPages { get; set; }
        public required List<int> Pages { get; set; }
    }

    public class PaginatedUserResponse
    {
        public required PaginationResponse Pagination { get; set; }
        public required IReadOnlyCollection<AdminUserResponse> Users { get; set; }
    }
    #endregion

}
