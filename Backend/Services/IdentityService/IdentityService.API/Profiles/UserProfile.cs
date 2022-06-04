using AutoMapper;
using IdentityService.Contracts.v1;
using IdentityService.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, AuthenticatedUserResponse>();
            CreateMap<ApplicationUser, AuthenticatedUserUserResponse>();
        }
    }
}
