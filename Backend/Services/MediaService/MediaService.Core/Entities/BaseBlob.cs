using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Core.Entities
{
    public class BaseBlob : AggregateRoot
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public long Size { get; set; }
        public string Format { get; set; }
        public string FileName { get; set; }

        
    }
}
