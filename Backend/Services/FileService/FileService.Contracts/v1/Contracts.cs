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
    public record UploadImageRequest([Required] Guid ExternalEntityId, [Required] IFormFile Image, string? Title, string? Description, string? Tags);
    public record PatchImageDescriptionRequest(string? Title, string? Description, string? Tags);

    // video request
    public record UploadVideoRequest([Required] Guid ExternalEntityId, [Required] IFormFile Video, string? Title, string? Description, string? Tags);
    public record PatchVideoDescriptionRequest(string? Title, string? Description, string? Tags);


    // image response
    public record ImageResponse(Guid Id, string Title, string Description, string Tags, List<ImageUrlResponse> Images, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt, Guid ExternalEntityId);
    public record ImageUrlResponse(int Breakpoint, string Url, string Format, long Size, int Width, int Height);

    // video response
    public record VideoResponse(Guid Id, string Title, string Description, string Tags, VideoUrlResponse Url, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt, Guid ExternalEntityId);
    public record VideoUrlResponse(string Url, string Format, int Duration, long Size, int Width, int Height);
}
