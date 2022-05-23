using MediaService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Commands
{
    public class CreateMediaFileCommand : IRequest<MediaFile>
    {
        public MediaFile NewMediaFile { get; set; } = default!;
    }
}
