using AutoMapper;
using FileService.Contracts.v1;
using FileService.Contracts.v1.Contracts;
using FileService.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.API.Profiles
{
    public class AvatarProfile : Profile
    {
        public AvatarProfile()
        {
            CreateMap<Avatar, UploadAvatarResponse>();
        }
    }
}
