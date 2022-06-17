using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1.Contracts
{
    // Requests

    public record UploadProductImageRequest([Required] Guid ExternalEntityId, [Required] IFormFile Image, string? Title, string? Description, string? Tags);
    public record UploadProductVideoRequest([Required] Guid ExternalEntityId, [Required] IFormFile Video, string? Title, string? Description, string? Tags);
    public record PatchProductImageDescriptionRequest(string? Title, string? Description, string? Tags);


    public record UploadSetImageRequest([Required] Guid ExternalEntityId, [Required] IFormFile Image, string? Title, string? Description, string? Tags);
    public record UploadSetVideoRequest([Required] Guid ExternalEntityId, [Required] IFormFile Video, string? Title, string? Description, string? Tags);
    public record PatchSetImageDescriptionRequest(string? Title, string? Description, string? Tags);


    public record UploadCategoryImageRequest([Required] Guid ExternalEntityId, [Required] IFormFile Image, string? Title, string? Description, string? Tags);
    public record UploadCategoryVideoRequest([Required] Guid ExternalEntityId, [Required] IFormFile Video, string? Title, string? Description, string? Tags);
    public record PatchCategoryImageDescriptionRequest(string? Title, string? Description, string? Tags);



    public record UploadAvatarRequest([Required] IFormFile Image, [Required] Guid ExternalEntityId);


    public record UploadTenantBrandImageRequest([Required] IFormFile Image, [Required] Guid ExternalEntityId);
    public record UploadTenantLogoRequest([Required] IFormFile Image, [Required] Guid ExternalEntityId);
    public record UploadTenantVideoRequest([Required] IFormFile Video, [Required] Guid ExternalEntityId);
    public record UploadTenantBannerRequest([Required] IFormFile Image, [Required] Guid ExternalEntityId);



    // responses
    public record AssetResponse(Guid Id, Guid ExternalEntityId, int AssetTypeValue, string AssetTypeDescription, string Type, string? Title, string? Description, string? Tags, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt)
    {
        public VideoAssetUrlResponse? Video { get; private set; } = null!;
        public IEnumerable<ImageAssetUrlResponse>? Images { get; private set; } = null!;
    }
    public record ImageAssetUrlResponse(string Url, int Breakpoint, string Format, long Size, int Width, int Height);
    public record VideoAssetUrlResponse(string Url, string Format, int Duration, long Size, int Width, int Height);


    public record AvatarResponse(Guid Id, Guid ExternalEntityId, int AssetTypeValue, string AssetTypeDescription, string Url, string Type, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);

    public record TenantResponse(Guid Id, Guid ExternalEntityId, int AssetTypeValue, string AssetTypeDescription, string Url, string Type, DateTimeOffset CreatedAt, DateTimeOffset? ModifiedAt);


}
