using Microsoft.EntityFrameworkCore;
using PaymentService.Core.Entities;
using PaymentService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _paymentContext;

        public PaymentRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            _paymentContext.Add(payment);
            await _paymentContext.SaveChangesAsync();
            return payment;
        }

        public async Task<IEnumerable<Payment>> ListPaymentsAsync()
        {
            return await _paymentContext.Payments.ToListAsync();
        }
    }
}
