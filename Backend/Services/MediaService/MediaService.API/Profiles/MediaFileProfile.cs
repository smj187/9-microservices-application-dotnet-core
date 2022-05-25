using AutoMapper;
using MediaService.Contracts.v1.Requests;
using MediaService.Contracts.v1.Responses;
using MediaService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.API.Profiles
{
    public class MediaFileProfile : Profile
    {
        public MediaFileProfile()
        {
            CreateMap<CreateFileRequest, Blob>();
            CreateMap<Blob, FileResponse>();
        }
    }
}
