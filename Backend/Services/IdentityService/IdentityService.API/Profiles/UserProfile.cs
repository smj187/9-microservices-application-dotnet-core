using AutoMapper;
using BuildingBlocks.EfCore.Helpers;
using CatalogService.API.Profiles;
using IdentityService.API.Profiles.ActionMappings;
using IdentityService.Application.DTOs;
using IdentityService.Contracts.v1;
using IdentityService.Core.Identities;
using IdentityService.Core.Models;
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
                .ForMember(dest => dest.Token, opts => opts.MapFrom(s => s.Token))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(s => s.Roles))
                .AfterMap<SetRegistrationTokenExpirationMappingAction>();

            CreateMap<InternalUserModel, AdminUserResponse>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(s => s.InternalIdentityUser.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(s => s.InternalIdentityUser.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(s => s.InternalIdentityUser.UserName))
                .ForMember(dest => dest.LockoutEnd, opts => opts.MapFrom(s => s.InternalIdentityUser.LockoutEnd))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(s => s.Roles))
                .ForMember(dest => dest.CreatedAt, opts => opts.MapFrom(s => s.ApplicationUser.CreatedAt))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.ModifiedAt, opts => opts.MapFrom(s => s.ApplicationUser.ModifiedAt))
                .ForMember(dest => dest.Firstname, opts => opts.MapFrom(s => s.ApplicationUser.Firstname))
                .ForMember(dest => dest.Lastname, opts => opts.MapFrom(s => s.ApplicationUser.Lastname))
                .ForMember(dest => dest.AvatarUrl, opts => opts.MapFrom(s => s.ApplicationUser.AvatarUrl));

            CreateMap<Role, string>();

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


            CreateMap<PaginatedUsersResponseDTO, PaginatedUserResponse>()
                .AfterMap<AddPaginatedProductsMappingAction>();

        }
    }
}
