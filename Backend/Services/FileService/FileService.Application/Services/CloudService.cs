using BuildingBlocks.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FileService.Core.Domain.Image;
using FileService.Core.Domain.User;
using FileService.Core.Domain.Video;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileService.Application.Services
{
    public static class FileExtensions
    {
        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private static readonly FileExtensionContentTypeProvider Provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string fileName)
        {
            if (!Provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";

            }

            return contentType;
        }
    }

    public class CloudService : ICloudService
    {
        private readonly Cloudinary _cloudinary;

        public CloudService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<VideoUrl> UploadVideoAsync(string folder, IFormFile file, string title, string? description, string? tags)
        {
            // check valid file type
            var validTypes = new List<string> { "video/mp4" };
            if (!validTypes.Contains(file.ContentType))
            {
                throw new DomainViolationException($"{file.ContentType} is not supported");
            }

            // get stream
            var bytes = await file.GetBytes();
            using var inStream = new MemoryStream(bytes);

            // clear up title
            var pattern = new Regex(@".mp4");
            title = pattern.Replace(title, "");
            var fileDescription = new FileDescription(WebUtility.UrlEncode(title), inStream);

            var uploadParams = new VideoUploadParams()
            {
                File = fileDescription,
                PublicIdPrefix = folder,
                PublicId = WebUtility.UrlEncode(title),
                Overwrite = true,
                Tags = tags,
            };

            var response = await _cloudinary.UploadAsync(uploadParams);

            return new VideoUrl(response.SecureUrl.ToString(), response.Format, response.Duration, response.Bytes, response.Width, response.Height);

        }

        public async Task<IEnumerable<ImageUrl>> UploadImageAsync(string folder, IFormFile file, string title, string? description, string? tags)
        {
            // check valid file type
            var validTypes = new List<string> { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!validTypes.Contains(file.ContentType))
            {
                throw new DomainViolationException($"{file.ContentType} is not supported");
            }

            // get stream
            var bytes = await file.GetBytes();
            using var inStream = new MemoryStream(bytes);

            // get image width
            using var img = SixLabors.ImageSharp.Image.Load(inStream);
            inStream.Position = 0;
            var width = img.Width;

            // clear up title
            var pattern = new Regex(@".jpeg|.jpg|.png|.gif|.webp");
            title = pattern.Replace(title, "");
            var fileDescription = new FileDescription(WebUtility.UrlEncode(title), inStream);

            ImageUploadResult response;
            var format = file.ContentType.Split("image/").LastOrDefault();

            var uploadParams = new ImageUploadParams()
            {
                File = fileDescription,
                PublicIdPrefix = folder,
                PublicId = WebUtility.UrlEncode(title),
                Overwrite = true,
                Tags = tags,
            };

            if (file.ContentType == "image/gif")
            {
                uploadParams.Format = format;
                response = await _cloudinary.UploadAsync(uploadParams);
            }
            else
            {
                uploadParams.Format = "webp"; // format all files to webp
                uploadParams.ResponsiveBreakpoints = GetBreakpoints(width);
                response = await _cloudinary.UploadAsync(uploadParams);
            }


            var urls = new List<ImageUrl>();
            var newFileName = $"/{folder}/{WebUtility.UrlEncode(title)}.{response.Format}";
            if (response.ResponsiveBreakpoints != null)
            {
                foreach (var breakpoint in response.ResponsiveBreakpoints)
                {
                    var imageObj = breakpoint.Breakpoints.FirstOrDefault();
                    if (imageObj == null)
                    {
                        throw new DomainViolationException("could not find any uploaded image urls");
                    }

                    var url = Regex.Replace(imageObj.SecureUrl, @"\/" + folder + @"\/(.*)\.webp", newFileName);
                    urls.Add(new ImageUrl(imageObj.Width, url, response.Format, imageObj.Bytes, imageObj.Width, imageObj.Height));
                }
            }
            else
            {
                urls.Add(new ImageUrl(response.Width, response.SecureUrl.AbsoluteUri, response.Format, response.Bytes, response.Width, response.Height));
            }

            return urls;
        }


        private static List<ResponsiveBreakpoint> GetBreakpoints(int imageWidth)
        {
            var breakpoints = new List<ResponsiveBreakpoint>();
            if (imageWidth > 480)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(480).MinWidth(480));
            }
            else
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1));
            }

            if (imageWidth > 748)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(748).MinWidth(748));
            }

            if (imageWidth > 1280)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(1280).MinWidth(1280));
            }

            if (imageWidth > 1920)
            {
                breakpoints.Add(new ResponsiveBreakpoint().MaxImages(1).MaxWidth(1920).MinWidth(1920));
            }

            return breakpoints;
        }

        public async Task<string> UploadUserAvatarAsync(string folder, IFormFile file, Guid userId)
        {
            // check valid file type
            var validTypes = new List<string> { "image/jpeg", "image/jpg", "image/png", "image/gif", "image/webp" };
            if (!validTypes.Contains(file.ContentType))
            {
                throw new DomainViolationException($"{file.ContentType} is not supported");
            }

            // get stream
            var bytes = await file.GetBytes();
            using var inStream = new MemoryStream(bytes);


            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"avatar_{userId}", inStream),
                PublicIdPrefix = folder,
                PublicId = $"avatar_{userId}",
                Overwrite = true,
                Format = file.ContentType.Split("image/").LastOrDefault()
            };

            var response = await _cloudinary.UploadAsync(uploadParams);

            return response.SecureUrl.ToString();
        }
    }
}
