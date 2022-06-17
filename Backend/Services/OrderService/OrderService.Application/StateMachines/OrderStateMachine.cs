using BuildingBlocks.MassTransit;
using BuildingBlocks.MassTransit.Commands;
using CatalogService.Contracts.v1.Events;
using DeliveryService.Contracts.v1.Events;
using MassTransit;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Core.Entities.Enumerations;
using PaymentService.Contracts.v1.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Events;

namespace OrderService.Application.StateMachines
{
    public class OrderStateMachine : MassTransitStateMachine<OrderStateMachineInstance>
    {
        // states
        public State AwaitingItemAllocationState { get; set; }
        public State ItemOutOfStockFailureState { get; set; }

        public State PaymentState { get; set; }
        public State PaymentFailureState { get; set; }

        public State TenantState { get; set; }
        public State TenantRejectionState { get; set; }


        public State DeliveryState { get; set; }


        public State OrderCompleteState { get; set; }




        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreateOrderSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => ItemAllocationSuccessEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => ItemAllocationOutOfStockEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => ItemAllocationNotVisibleEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => PaymentFailureEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentSuccessEvent, x => x.CorrelateById(context => context.Message.OrderId));


            Event(() => TenantApproveOrderEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => TenantRejectOrderEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => DeliverySuccessEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => DeliveryFailureEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => CheckOrderEvent, x =>
            {
                x.CorrelateById(context => context.Message.OrderId);

                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    await context.RespondAsync(new OrderNotFoundResponse
                    {
                        OrderId = context.Message.OrderId,
                        Message = $"the order is already complete"
                    });
                }));
            });


            Initially(
                When(CreateOrderSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("init");
                        context.Saga.CorrelationId = context.Message.OrderId;
                        context.Saga.UserId = context.Message.UserId;
                        context.Saga.OrderId = context.Message.OrderId;
                        context.Saga.Items = context.Message.Items;

                        context.Saga.OrderStatus = OrderStatus.Created;
                    })
                    .TransitionTo(AwaitingItemAllocationState)
                    .Send(new Uri($"queue:{RabbitMqSettings.CatalogAllocationEndpointName}"), context => new ItemAllocationCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.Items))
                );


            During(AwaitingItemAllocationState,
                When(ItemAllocationSuccessEvent)
                    .Then(context =>
                    {
                        var total = context.Message.Items.Sum(x => x.Quantity * x.Price);
                        Console.WriteLine($"item is in stock | Total: {total}");
                        context.Saga.TotalAmount = total ?? 0;
                        context.Saga.OrderStatus = OrderStatus.ItemsAllocated;
                    })
                    .TransitionTo(PaymentState)
                    .Send(new Uri($"queue:{RabbitMqSettings.PaymentConsumerEndpointName}"), context => new PaymentCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.TotalAmount)),

                When(ItemAllocationOutOfStockEvent)
                    .Then(context =>
                    {
                        var items = context.Message.Items.Select(x => x.ToString());
                        Console.WriteLine($"item(s) are not in stock :( {string.Join(", ", items)}");

                        context.Saga.OrderStatus = OrderStatus.ItemsOutOfStock;
                    })
                    .Finalize(),

                When(ItemAllocationNotVisibleEvent)
                    .Then(context =>
                    {
                        var items = context.Message.Items.Select(x => x.ToString());
                        Console.WriteLine($"the following items are not available {string.Join(", ", items)}");

                        context.Saga.OrderStatus = OrderStatus.ItemNotVisible;
                    })
                    .Finalize()
                );


            During(PaymentState,
                When(PaymentSuccessEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"user {context.Saga.UserId} paid a total of {context.Saga.TotalAmount} for: ");
                        foreach (var item in context.Saga.Items)
                        {
                            Console.WriteLine($"    -> {item}");
                        }

                        context.Saga.OrderStatus = OrderStatus.PaymentSuccess;
                    })
                    .TransitionTo(TenantState)
                    .Send(new Uri($"queue:{RabbitMqSettings.TenantConsumerEndpointName}"), context => new TenantCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.UserId, context.Saga.Items, context.Saga.TotalAmount)),

                When(PaymentFailureEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("payment failed :(");
                        context.Saga.OrderStatus = OrderStatus.PaymentFailure;
                    })
                    .Finalize()
                );

            During(TenantState,
                When(TenantApproveOrderEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"tenant approved order for {context.Saga.UserId} for {context.Saga.Items.Count} items");

                        context.Saga.OrderStatus = OrderStatus.TenantApproved;
                    })
                    .TransitionTo(DeliveryState)
                    .Send(new Uri($"queue:{RabbitMqSettings.DeliveryConsumerEndpointName}"), context => new DeliveryCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.UserId, context.Saga.Items)),

                When(TenantRejectOrderEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("tenant approval failed :(");
                        context.Saga.OrderStatus = OrderStatus.TenantRejected;
                    })
                    .Finalize()
                );


            During(DeliveryState,
                When(DeliverySuccessEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"order {context.Saga.OrderId} was delivered successfully. this saga has ended");
                        context.Saga.OrderStatus = OrderStatus.DeliverySuccess;
                    })
                    .TransitionTo(OrderCompleteState)
                    .Finalize(),

                When(DeliveryFailureEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("delviery failed :(");
                        context.Saga.OrderStatus = OrderStatus.DeliveryFailed;
                    })
                    .Finalize()
                );


            DuringAny(
               When(CheckOrderEvent)
                   .RespondAsync(context => context.Init<CheckOrderStatusResponse>(new CheckOrderStatusResponse
                   {
                       UserId = context.Saga.UserId,
                       OrderId = context.Saga.OrderId,
                       Items = context.Saga.Items,
                       Status = context.Saga.OrderStatus.Description
                   }))
                );


            SetCompletedWhenFinalized();
        }


        // 0 stage -> create order saga
        public Event<CreateOrderSagaEvent> CreateOrderSagaEvent { get; private set; }


        // 1st stage -> check if items are available to buy
        public Event<CatalogAllocationSuccessEvent> ItemAllocationSuccessEvent { get; private set; }
        public Event<CatalogAllocationOutOfStockEvent> ItemAllocationOutOfStockEvent { get; private set; }
        public Event<CatalogAllocationNotVisibleEvent> ItemAllocationNotVisibleEvent { get; private set; }


        // 2nd stage -> payment process
        public Event<PaymentSuccessEvent> PaymentSuccessEvent { get; private set; }
        public Event<PaymentFailureEvent> PaymentFailureEvent { get; private set; }


        // 3rd stage -> tenant approves or rejects the order
        public Event<TenantApproveOrderEvent> TenantApproveOrderEvent { get; private set; }
        public Event<TenantRejectOrderEvent> TenantRejectOrderEvent { get; private set; }


        // 4th stage -> delivery
        public Event<DeliverySuccessEvent> DeliverySuccessEvent { get; private set; }
        public Event<DeliveryFailureEvent> DeliveryFailureEvent { get; private set; }



        public Event<CheckOrderStatusEvent> CheckOrderEvent { get; private set; }
    }
}
