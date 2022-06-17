using BuildingBlocks.Domain.EfCore;
using BuildingBlocks.Domain.Interfaces;
using BuildingBlocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public interface IPaymentRepository<T> : IEfRepository<T> where T : IAggregateRoot
    {

    }
}
