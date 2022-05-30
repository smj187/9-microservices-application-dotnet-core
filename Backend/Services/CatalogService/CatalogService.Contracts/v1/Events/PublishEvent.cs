using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Contracts.v1.Events
{
    public class PublishEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
