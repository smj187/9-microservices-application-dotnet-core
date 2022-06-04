using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Contracts.v1
{
    // requests
    public record UserRegisterRequest([Required] string Username, [Required] string Email, [Required] string EmailConfirm, [Required] string Password, [Required] string PasswordConfirm, string? Firstname, string? Lastname);
    public record UserLoginRequest([Required] string Email, [Required] string Password);

    public record AddRoleToUserRequest([Required] List<string> Roles);
    public record RemoveRoleFromUserRequest([Required] List<string> Roles);

    // responses






    public class AuthenticateUserRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    




    public class AuthenticatedUserResponse
    {
        public string Token { get; set; }

        public AuthenticatedUserUserResponse User { get; set; }
    }

    public class AuthenticatedUserUserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }

    }
}
