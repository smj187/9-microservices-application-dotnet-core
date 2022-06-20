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
            //CreateMap<ApplicationUser, AuthenticatedUserResponse>();
            //CreateMap<ApplicationUser, AuthenticatedUserUserResponse>();


            // responses

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

            //CreateMap<RefreshToken, AdminRefreshTokenResponse>();
            //CreateMap<ApplicationUser, AdminUserResponse>()
            //    .ForMember(dest => dest.RefreshTokens, opts => opts.MapFrom(s => s.RefreshTokens))
            //    .ForMember(dest => dest.LockoutEnd, opts => opts.MapFrom(s => s.LockoutEnd));

            //CreateMap<ApplicationUser, UserResponse>();
            //CreateMap<AuthenticatedUser, LoginUserResponse>();
            //CreateMap<AuthenticatedUser, RegisterUserResponse>();
            //.ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.Uer.Id));
            //.ForMember(dest => dest.Lastname, opts => opts.MapFrom(s => s.User.Lastname))
            //.ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.User.AvatarUrl))
            //.ForMember(dest => dest.Username, opts => opts.MapFrom(s => s.User.UserName))
            //.ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.User.Email))
            //.ForMember(dest => dest.Roles, opts => opts.MapFrom(s => s.User.Roles))
            //.ForMember(dest => dest.Token, opts => opts.MapFrom(s => s.Token));
            //.ForMember(dest => dest.RefreshToken, opts => opts.MapFrom(s => s.RefreshToken))
            //.ForMember(dest => dest.RefreshTokenExpiration, opts => opts.MapFrom(s => s.RefreshTokenExpiration));
        }
    }
}
