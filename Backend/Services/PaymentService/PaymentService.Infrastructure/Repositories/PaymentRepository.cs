using BuildingBlocks.Domain;
using BuildingBlocks.EfCore.Repositories;
using PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository<T> : EfRepository<T>, IPaymentRepository<T> where T : AggregateBase
    {
        public PaymentRepository(PaymentContext context)
            : base(context)
        {

        }
    }
}
