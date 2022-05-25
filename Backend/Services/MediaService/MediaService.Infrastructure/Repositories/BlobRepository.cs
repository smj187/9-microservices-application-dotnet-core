using BuildingBlocks.Domain;
using BuildingBlocks.EfCore;
using MediaService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Infrastructure.Repositories
{
    public class BlobRepository<T> : Repository<T>, IBlobRepository<T> where T : AggregateRoot
    {
        public BlobRepository(MediaContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }

    }
}
