using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediaService.Application.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;
using System.Text.RegularExpressions;

namespace MediaService.Application.Services
{
    public class CloudService : ICloudService
    {
        private readonly Cloudinary _cloudinary;

        public CloudService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }


        public async Task<ImageUploadResult> UploadImageAsync(string folder, IFormFile file, string title, string description, string tags)
        {
            // check valid file type
            var validTypes = new List<string> { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!validTypes.Contains(file.ContentType))
            {
                throw new Exception($"{file.ContentType} is not supported");
            }

            // get stream
            var bytes = await file.GetBytes();
            using var inStream = new MemoryStream(bytes);

            // get image width
            using var img = SixLabors.ImageSharp.Image.Load(inStream);
            inStream.Position = 0;
            var width = img.Width;

            if (file.ContentType == "image/gif")
            {
                return await _cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(WebUtility.UrlEncode(title), inStream),
                    PublicIdPrefix = folder,
                    PublicId = WebUtility.UrlEncode(title),
                    Overwrite = true,
                    Tags = tags,
                    AssetFolder = "public_assets/images",
                    Format = file.ContentType.Split("image/").LastOrDefault(),
                });
            }

            // build responsive breakpoints 
            var breakpoints = new List<ResponsiveBreakpoint>();
            if (width > 480)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(480).MinWidth(480));
            }
            else
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1));
            }

            if (width > 748)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(748).MinWidth(748));
            }

            if (width > 1280)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(1280).MinWidth(1280));
            }
            
            if (width > 1920)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(1920).MinWidth(1920));
            }

            return await _cloudinary.UploadAsync(new ImageUploadParams()
            {
                File = new FileDescription(WebUtility.UrlEncode(title), inStream),
                PublicIdPrefix = folder,
                PublicId = WebUtility.UrlEncode(title),
                Overwrite = true,
                Tags = tags,
                AssetFolder = "public_assets",
                Format = "webp",
                ResponsiveBreakpoints = breakpoints
            });
        }

    }
}
