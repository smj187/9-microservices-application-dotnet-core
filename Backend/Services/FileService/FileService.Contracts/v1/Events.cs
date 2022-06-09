using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Contracts.v1.Events
{
    public record GroupImageUploadSuccessEvent(Guid ProductId, Guid ImageId);
    public record ProductImageUploadSuccessEvent(Guid ProductId, Guid ImageId);

    public record AvatarUploadSuccessEvent(Guid UserId, string Url);

}
