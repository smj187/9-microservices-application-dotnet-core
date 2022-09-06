using AutoMapper;
using IdentityService.Contracts.v1.Contracts;
using IdentityService.Core.Aggregates;
using IdentityService.Core.Models;
using Microsoft.AspNetCore.Identity;
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

            CreateMap<InternalUserModel, TokenResponse>();

            CreateMap<InternalUserModel, UserResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.InternalIdentityUser.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.InternalIdentityUser.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(s => s.InternalIdentityUser.UserName))
                .ForMember(dest => dest.Firstname, opts => opts.MapFrom(s => s.ApplicationUser.Firstname))
                .ForMember(dest => dest.Lastname, opts => opts.MapFrom(s => s.ApplicationUser.Lastname))
                .ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.ApplicationUser.AvatarUrl))
                .ForMember(dest => dest.Token, opts => opts.MapFrom(s => s.Token));

            CreateMap<InternalUserModel, AdminUserResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.InternalIdentityUser.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.InternalIdentityUser.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(s => s.InternalIdentityUser.UserName))
                .ForMember(dest => dest.LockoutEnd, opts => opts.MapFrom(s => s.InternalIdentityUser.LockoutEnd))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(s => s.ApplicationUser.CreatedAt))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.Firstname, opts => opts.MapFrom(s => s.ApplicationUser.Firstname))
                .ForMember(dest => dest.Lastname, opts => opts.MapFrom(s => s.ApplicationUser.Lastname))
                .ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.ApplicationUser.AvatarUrl));

            CreateMap<InternalUserModel, UserProfileResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.InternalIdentityUser.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.InternalIdentityUser.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(s => s.InternalIdentityUser.UserName))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(s => s.ApplicationUser.CreatedAt))
                .ForMember(dest => dest.LockoutEnd, opts => opts.MapFrom(s => s.InternalIdentityUser.LockoutEnd))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.Firstname, opts => opts.MapFrom(s => s.ApplicationUser.Firstname))
                .ForMember(dest => dest.Lastname, opts => opts.MapFrom(s => s.ApplicationUser.Lastname))
                .ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.ApplicationUser.AvatarUrl));
          
        }
    }
}
