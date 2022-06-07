using BuildingBlocks.Domain.EfCore;
using FileService.Core.Domain.User;
using FileService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Repositories
{
    public class AvatarRepository : EfRepository<Avatar>, IAvatarRepository
    {
        public AvatarRepository(FileContext context)
            : base(context)
        {

        }
    }
}
