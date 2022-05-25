using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.Entities
{
    public class Order : AggregateRoot
    {
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public bool Success { get; set; }

        public List<Guid> Productds { get; set; } = new();
    }
}
