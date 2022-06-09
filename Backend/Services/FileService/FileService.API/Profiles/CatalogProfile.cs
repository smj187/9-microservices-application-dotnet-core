using AutoMapper;
using FileService.Contracts.v1.Contracts;
using FileService.Core.Domain;
using FileService.Core.Domain.Aggregates;
using FileService.Core.Domain.Aggregates.Image;
using FileService.Core.Domain.Aggregates.Video;
using FileService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {

            new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
            });

            CreateMap<AssetType, Int32>();
            CreateMap<ImageUrl, ImageAssetUrlResponse>();
            CreateMap<VideoUrl, VideoAssetUrlResponse>();




            CreateMap<AssetFile, AssetResponse>(MemberList.Destination);

            CreateMap<VideoAsset, AssetResponse>()
                .IncludeBase<AssetFile, AssetResponse>()
                .ForMember(dest => dest.AssetTypeValue, opts => opts.MapFrom(s => s.AssetType.Value))
                .ForMember(dest => dest.AssetTypeDescription, opts => opts.MapFrom(s => s.AssetType.Description));

            CreateMap<ImageAsset, AssetResponse>()
                .IncludeBase<AssetFile, AssetResponse>()
                .ForMember(dest => dest.AssetTypeValue, opts => opts.MapFrom(s => s.AssetType.Value))
                .ForMember(dest => dest.AssetTypeDescription, opts => opts.MapFrom(s => s.AssetType.Description));
        }
    }
}
