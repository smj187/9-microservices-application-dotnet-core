using AutoMapper;
using FileService.Contracts.v1;
using FileService.Contracts.v1.Contracts;
using FileService.Core.Domain.Aggregates.Avatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<AvatarAsset, AvatarResponse>()
                .ForMember(dest => dest.AssetTypeValue, opts => opts.MapFrom(s => s.AssetType.Value))
                .ForMember(dest => dest.AssetTypeDescription, opts => opts.MapFrom(s => s.AssetType.Description));
        }
    }
}
