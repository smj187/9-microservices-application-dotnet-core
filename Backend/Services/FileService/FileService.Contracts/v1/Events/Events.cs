using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1.Events
{

    public record ProductImageUploadResponseEvent(string TenantId, Guid ProductId, Guid ImageId);
    public record ProductVideoUploadResponseEvent(string TenantId, Guid ProductId, Guid VideoId);

    public record SetImageUploadResponseEvent(string TenantId, Guid SetId, Guid ImageId);
    public record SetVideoUploadResponseEvent(string TenantId, Guid SetId, Guid VideoId);

    public record CategoryImageUploadResponseEvent(string TenantId, Guid CategoryId, Guid ImageId);
    public record CategoryVideoUploadResponseEvent(string TenantId, Guid CategoryId, Guid VideoId);


    public record AvatarUploadResponseEvent(string TenantId, Guid UserId, string Url);



    public record TenantBrandImageUploadResponseEvent(string TenantId, Guid ImageId);
    public record TenantLogoUploadResponseEvent(string TenantId, Guid ImageId);
    public record TenantVideoUploadResponseEvent(string TenantId, Guid VideoId);
    public record TenantBannerUploadResponseEvent(string TenantId, Guid ImageId);

}
