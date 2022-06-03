using BuildingBlocks.Domain.EfCore;
using FileService.Core.Domain.Image;
using FileService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Repositories
{
    public class ImageRepository : EfRepository<ImageFile>, IImageRepository
    {
        public ImageRepository(FileContext context) 
            : base(context)
        {

        }
    }
}
