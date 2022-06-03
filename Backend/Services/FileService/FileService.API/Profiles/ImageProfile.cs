using AutoMapper;
using FileService.Contracts.v1;
using FileService.Core.Domain.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // requests
            CreateMap<UploadImageRequest, ImageFile>()
                .ConstructUsing((src, ctx) =>
                {
                    return new ImageFile(src.Title, src.Description, src.Tags);
                });


            // responses
            CreateMap<ImageFile, ImageResponse>();
            CreateMap<ImageUrl, ImageUrlResponse>();
        }
    }
}
