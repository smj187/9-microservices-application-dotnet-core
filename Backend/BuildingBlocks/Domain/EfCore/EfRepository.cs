using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain.EfCore
{
    public class EfRepository<T> : BaseRepository<T> where T : AggregateRoot
    {
        public EfRepository(DbContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }
    }
}
