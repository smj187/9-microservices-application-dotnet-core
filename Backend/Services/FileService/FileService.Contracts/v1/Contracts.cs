using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1
{
    // image request
    public record UploadImageRequest(string? Title, string? Description, string? Tags, [Required] IFormFile Image);
    public record PatchImageDescriptionRequest(string? Title, string? Description, string? Tags);

    // video request
    public record UploadVideoRequest(string? Title, string? Description, string? Tags, [Required] IFormFile Video);
    public record PatchVideoDescriptionRequest(string? Title, string? Description, string? Tags);


    // image response
    public record ImageResponse(Guid Id, string Title, string Description, string Tags, List<ImageUrlResponse> Images, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);
    public record ImageUrlResponse(int Breakpoint, string Url, string Format, long Size, int Width, int Height);

    // video response
    public record VideoResponse(Guid Id, string Title, string Description, string Tags, VideoUrlResponse Url);
    public record VideoUrlResponse(string Url, string Format, int Duration, long Size, int Width, int Height);
}
