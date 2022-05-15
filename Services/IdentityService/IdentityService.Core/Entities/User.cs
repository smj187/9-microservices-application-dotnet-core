using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentityService.Core.Entities
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

        public User()
        {
            Roles = new List<string>();
            RefreshTokens = new List<RefreshToken>();
        }
    }
}
