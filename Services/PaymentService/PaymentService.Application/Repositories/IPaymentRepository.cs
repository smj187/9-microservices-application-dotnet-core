using PaymentService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> ListPaymentsAsync();
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
