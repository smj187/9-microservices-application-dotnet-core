using AutoMapper;
using FileService.Contracts.v1;
using FileService.Core.Domain.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            // requests
            CreateMap<UploadVideoRequest, VideoFile>()
                .ConstructUsing((src, ctx) =>
                {
                    return new VideoFile(src.Title, src.Description, src.Tags);
                });


            // responses
            CreateMap<VideoFile, VideoResponse>();

            CreateMap<VideoUrl, VideoUrlResponse>();
        }
    }
}
