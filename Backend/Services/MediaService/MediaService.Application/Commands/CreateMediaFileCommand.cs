using MediaService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Commands
{
    public class CreateMediaFileCommand : IRequest<Blob>
    {
        public Blob NewMediaFile { get; set; } = default!;
    }
}
