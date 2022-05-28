using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Services
{
    public interface ICloudService
    {
        Task<ImageUploadResult> UploadImageAsync(string tenant, IFormFile file, string title, string description, string tags);
    }
}
