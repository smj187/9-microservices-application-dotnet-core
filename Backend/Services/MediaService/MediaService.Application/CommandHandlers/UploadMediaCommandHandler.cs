using MediaService.Application.Commands;
using MediaService.Application.Services;
using MediaService.Core.Entities;
using MediaService.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MediaService.Application.CommandHandlers
{
    public class UploadMediaCommandHandler : IRequestHandler<UploadMediaCommand, Object>
    {
        private readonly ICloudService _cloudService;
        private readonly MediaContext _context;

        public UploadMediaCommandHandler(ICloudService cloudService, MediaContext context)
        {
            _cloudService = cloudService;
            _context = context;
        }

        public async Task<Object> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
        {
            var response = await _cloudService.UploadImageAsync(request.FolderName, request.File, request.Title, request.Description, request.Tags);

            var urls = new List<ImageUrl>();

            string newFileName = $"/{request.FolderName}/{WebUtility.UrlEncode(request.Title)}.{request.File.ContentType.Split("image/").LastOrDefault()}";
            if (response.ResponsiveBreakpoints != null)
            {
                foreach (var breakpoint in response.ResponsiveBreakpoints)
                {
                    var imageObj = breakpoint.Breakpoints.FirstOrDefault();
                    if (imageObj == null)
                    {
                        throw new Exception("could not find image");
                    }

                    var pattern = @"\/" + request.FolderName + @"\/(.*)\.webp";
                    urls.Add(new ImageUrl
                    {
                        Breakpoint = imageObj.Width,
                        Url = Regex.Replace(imageObj.SecureUrl, pattern, newFileName)
                    });
                }
            }
            else
            {
                urls.Add(new ImageUrl
                {
                    Breakpoint = response.Width,
                    Url = response.SecureUrl.AbsoluteUri
                });
            }


            var blob = new ImageBlob
            {
                Format = response.Format,
                Name = request.Title,
                Description = request.Description,
                FileName = newFileName,
                Size = response.Bytes,
                ImageUrls = urls
            };

            _context.Add(blob);
            await _context.SaveChangesAsync(cancellationToken);

            return blob;
        }
    }
}
