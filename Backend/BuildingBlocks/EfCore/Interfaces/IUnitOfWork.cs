using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.EfCore.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        void Dispose();
    }
}
