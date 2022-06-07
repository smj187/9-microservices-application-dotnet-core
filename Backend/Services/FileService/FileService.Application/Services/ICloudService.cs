using CloudinaryDotNet.Actions;
using FileService.Core.Domain.Image;
using FileService.Core.Domain.User;
using FileService.Core.Domain.Video;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Services
{
    public interface ICloudService
    {
        Task<string> UploadUserAvatarAsync(string folder, IFormFile file, Guid userId);

        Task<IEnumerable<ImageUrl>> UploadImageAsync(string folder, IFormFile file, string title, string? description, string? tags);

        Task<VideoUrl> UploadVideoAsync(string folder, IFormFile file, string title, string? description, string? tags);
    }   
}
