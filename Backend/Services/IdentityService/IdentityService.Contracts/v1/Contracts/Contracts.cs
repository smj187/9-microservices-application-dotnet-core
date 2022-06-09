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




    // responses
    public record RegisterUserResponse(UserResponse User, string Token, string RefreshToken, DateTimeOffset RefreshTokenExpiration);
    public record LoginUserResponse(UserResponse User, string? Token, string RefreshToken, DateTimeOffset RefreshTokenExpiration);
    public record UserResponse(Guid Id, string? Firstname, string? Lastname, string? AvatarUrl, string Username, string Email, List<string> Roles);


    public record AdminUserResponse(Guid Id, string? Firstname, string? Lastname, string? AvatarUrl, string Username, string Email, List<string> Roles, DateTimeOffset? LockoutEnd, List<AdminRefreshTokenResponse> RefreshTokens, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);
    public record AdminRefreshTokenResponse(string Token, DateTimeOffset ExpiresAt, bool IsExpired, DateTimeOffset CreatedAt, DateTimeOffset RevokedAt, bool IsActive);

}
