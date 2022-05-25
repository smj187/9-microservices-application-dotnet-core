using BuildingBlocks.Domain;
using BuildingBlocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore
{
    public class EfRepository<T> : Repository<T> where T : AggregateRoot
    {
        public EfRepository(DbContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }
    }
}
