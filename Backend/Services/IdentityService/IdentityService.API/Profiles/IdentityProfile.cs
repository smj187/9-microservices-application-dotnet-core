using AutoMapper;
using IdentityService.Contracts.v1.Requests;
using IdentityService.Contracts.v1.Responses;
using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Token, AuthenticateUserResponse>();

            CreateMap<AuthenticateUserRequest, Token>();
        }
    }
}
