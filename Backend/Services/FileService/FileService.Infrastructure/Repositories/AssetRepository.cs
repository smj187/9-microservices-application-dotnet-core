using BuildingBlocks.Domain.EfCore;
using FileService.Core.Domain.Aggregates;
using FileService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure.Repositories
{
    public class AssetRepository : EfRepository<AssetFile>, IAssetRepository
    {
        public AssetRepository(FileContext context)
            : base(context)
        {

        }
    }
}
