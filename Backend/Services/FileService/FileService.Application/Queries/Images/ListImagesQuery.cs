using FileService.Core.Domain.Image;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Application.Queries.Images
{
    public class ListImagesQuery : IRequest<IReadOnlyCollection<ImageFile>>
    {

    }
}
