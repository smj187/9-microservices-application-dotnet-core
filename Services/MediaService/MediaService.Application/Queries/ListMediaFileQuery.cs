using MediaService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Application.Queries
{
    public class ListMediaFileQuery : IRequest<IEnumerable<MediaFile>>
    {

    }
}
