using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1.Events
{

    public record ProductImageUploadResponseEvent(Guid ProductId, Guid ImageId);
    public record ProductVideoUploadResponseEvent(Guid ProductId, Guid VideoId);

    public record SetImageUploadResponseEvent(Guid SetId, Guid ImageId);
    public record SetVideoUploadResponseEvent(Guid SetId, Guid VideoId);

    public record CategoryImageUploadResponseEvent(Guid CategoryId, Guid ImageId);
    public record CategoryVideoUploadResponseEvent(Guid CategoryId, Guid VideoId);


    public record AvatarUploadResponseEvent(Guid UserId, string Url);



    public record TenantBrandImageUploadResponseEvent(Guid TenantId, Guid ImageId);
    public record TenantLogoUploadResponseEvent(Guid TenantId, Guid ImageId);
    public record TenantVideoUploadResponseEvent(Guid TenantId, Guid VideoId);
    public record TenantBannerUploadResponseEvent(Guid TenantId, Guid ImageId);

}
