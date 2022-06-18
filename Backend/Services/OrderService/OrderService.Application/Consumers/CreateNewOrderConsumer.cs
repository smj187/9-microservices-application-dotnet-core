using BasketService.Contracts.v1.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class CreateNewOrderConsumer : IConsumer<BasketCheckoutCommand>
    {
        public CreateNewOrderConsumer()
        {

        }

        public Task Consume(ConsumeContext<BasketCheckoutCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}
