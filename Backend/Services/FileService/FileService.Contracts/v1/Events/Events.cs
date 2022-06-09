using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1.Events
{

    public record ProductImageUploadResponseEvent(Guid ProductId, Guid ImageId);
    public record ProductVideoUploadResponseEvent(Guid ProductId, Guid ImageId);

    public record GroupImageUploadResponseEvent(Guid GroupId, Guid ImageId);
    public record GroupVideoUploadResponseEvent(Guid GroupId, Guid ImageId);

    public record CategoryImageUploadResponseEvent(Guid CategoryId, Guid ImageId);
    public record CategoryVideoUploadResponseEvent(Guid CategoryId, Guid ImageId);


    public record AvatarUploadResponseEvent(Guid UserId, string Url);

}
