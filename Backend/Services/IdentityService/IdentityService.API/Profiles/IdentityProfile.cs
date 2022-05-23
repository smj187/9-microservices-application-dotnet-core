using AutoMapper;
using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
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
            CreateMap<Token, AuthenticateUserResponse>()
                .ForMember(dest => dest.Token, opts => opts.MapFrom(s => s.Jwt));

            CreateMap<AuthenticateUserRequest, Token>();
        }
    }
}
