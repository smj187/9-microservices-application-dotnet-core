using BuildingBlocks.Domain;
using BuildingBlocks.EfCore;
using PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository<T> : Repository<T>, IPaymentRepository<T> where T : AggregateRoot
    {
        public PaymentRepository(PaymentContext context)
            : base(new EfCommandRepository<T>(context), new EfQueryRepository<T>(context))
        {

        }
    }
}
