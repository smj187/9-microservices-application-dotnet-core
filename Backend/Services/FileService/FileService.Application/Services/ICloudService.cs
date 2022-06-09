using CloudinaryDotNet.Actions;
using FileService.Core.Domain.ValueObjects;
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

        Task<IEnumerable<ImageUrl>> UploadImageAsync(string folder, IFormFile file, string? title, string? description, string? tags);

        Task<VideoUrl> UploadVideoAsync(string folder, IFormFile file, string? title, string? description, string? tags);
    }   
}
