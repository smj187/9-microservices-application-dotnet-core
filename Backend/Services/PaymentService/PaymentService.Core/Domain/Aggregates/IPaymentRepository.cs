using BuildingBlocks.EfCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Core.Domain.Aggregates
{
    public interface IPaymentRepository : IEfRepository<Payment>
    {

    }
}
