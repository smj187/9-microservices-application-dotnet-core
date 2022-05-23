using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Core.Entities.Base
{
    public interface IMongoEntity
    {
        Guid Id { get; set; }
    }
}
