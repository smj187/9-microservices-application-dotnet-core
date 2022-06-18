using BuildingBlocks.MassTransit.Commands;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumers
{
    public class CreateNewOrderConsumer : IConsumer<CreateNewOrderCommand>
    {
        public CreateNewOrderConsumer()
        {

        }

        public Task Consume(ConsumeContext<CreateNewOrderCommand> context)
        {
            throw new NotImplementedException();
        }
    }
}
