using BuildingBlocks.EfCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Core.Domain.Aggregates
{
    public interface IAssetRepository : IEfRepository<AssetFile>
    {

    }
}
