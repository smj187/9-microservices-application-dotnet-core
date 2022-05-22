using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.Entities
{
    public class Order
    {
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public List<Guid> Products { get; set; } = new();
    }
}
