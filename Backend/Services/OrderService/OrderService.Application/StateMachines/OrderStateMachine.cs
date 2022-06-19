using BuildingBlocks.Masstransit;
using CatalogService.Contracts.v1.Commands;
using CatalogService.Contracts.v1.Events;
using DeliveryService.Contracts.v1.Commands;
using DeliveryService.Contracts.v1.Events;
using MassTransit;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Core.Entities.Enumerations;
using PaymentService.Contracts.v1.Commands;
using PaymentService.Contracts.v1.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Contracts.v1.Commands;
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

            Event(() => CreateOrderSagaEvent, x => x.CorrelateById(context => context.Message.BasketId));

            Event(() => CatalogAllocationSuccessSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => CatalogAllocationOutOfStockErrorSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => CatalogAllocationUnavailableErrorSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => PaymentFailureSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => PaymentSuccessSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));


            Event(() => TenantApproveOrderSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => TenantRejectOrderSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));

            Event(() => DeliverySuccessSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => DeliveryFailureSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));

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
                        context.Saga.CorrelationId = context.Message.BasketId;
                        context.Saga.OrderId = context.Message.BasketId;

                        context.Saga.UserId = context.Message.UserId;
                        context.Saga.Products = context.Message.Products;
                        context.Saga.Sets = context.Message.Sets;

                        context.Saga.OrderStatus = OrderStatus.Created;
                    })
                    .TransitionTo(AwaitingItemAllocationState)
                    .Send(new Uri($"queue:{RabbitMqSettings.OrderSagaCatalogConsumerEndpointName}"), context => new CatalogAllocationCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.Products, context.Saga.Sets))
                );


            During(AwaitingItemAllocationState,
                When(CatalogAllocationSuccessSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"products and sets are available.. continue");

                        context.Saga.TotalAmount = context.Message.Products.Sum(x => x.Price) + context.Message.Sets.Sum(x => x.Price);
                        context.Saga.OrderStatus = OrderStatus.ItemsAllocated;
                    })
                    .TransitionTo(PaymentState)
                    .Send(new Uri($"queue:{RabbitMqSettings.OrderSagaPaymentConsumerEndpointName}"), context => new PaymentCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.UserId, context.Saga.TotalAmount)),

                When(CatalogAllocationOutOfStockErrorSagaEvent)
                    .Then(context =>
                    {
                        var products = context.Message.Products.Select(x => x.ToString());
                        var sets = context.Message.Sets.Select(x => x.ToString());
                        Console.WriteLine($"-- out of stock -> products: {string.Join(", ", products)}");
                        Console.WriteLine($"-- out of stock -> sets: {string.Join(", ", sets)}");

                        context.Saga.OrderStatus = OrderStatus.ItemsOutOfStock;
                    })
                    .Finalize(),

                When(CatalogAllocationUnavailableErrorSagaEvent)
                    .Then(context =>
                    {
                        var products = context.Message.Products.Select(x => x.ToString());
                        var sets = context.Message.Sets.Select(x => x.ToString());
                        Console.WriteLine($"-- unavailable -> products: {string.Join(", ", products)}");
                        Console.WriteLine($"-- unavailable -> sets: {string.Join(", ", sets)}");

                        context.Saga.OrderStatus = OrderStatus.ItemNotVisible;
                    })
                    .Finalize()
                );


            During(PaymentState,
                When(PaymentSuccessSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"user {context.Saga.UserId} paid a total of {context.Saga.TotalAmount} for: ");
                        foreach (var item in context.Saga.Products)
                        {
                            Console.WriteLine($"    -> {item}");
                        }

                        context.Saga.OrderStatus = OrderStatus.PaymentSuccess;
                    })
                    .TransitionTo(TenantState)
                    .Send(new Uri($"queue:{RabbitMqSettings.OrderSagaTenantConsumerEndpointName}"), context => new TenantCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.UserId, context.Saga.Products, context.Saga.TotalAmount)),

                When(PaymentFailureSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("payment failed :(");
                        context.Saga.OrderStatus = OrderStatus.PaymentFailure;
                    })
                    .Finalize()
                );

            During(TenantState,
                When(TenantApproveOrderSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"tenant approved order for {context.Saga.UserId} for {context.Saga.Products.Count} items");

                        context.Saga.OrderStatus = OrderStatus.TenantApproved;
                    })
                    .TransitionTo(DeliveryState)
                    .Send(new Uri($"queue:{RabbitMqSettings.OrderSagaDeliveryConsumerEndpointName}"), context => new DeliveryCommand(context.Saga.CorrelationId, context.Saga.OrderId, context.Saga.UserId, context.Saga.Products)),

                When(TenantRejectOrderSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine("tenant approval failed :(");
                        context.Saga.OrderStatus = OrderStatus.TenantRejected;
                    })
                    .Finalize()
                );


            During(DeliveryState,
                When(DeliverySuccessSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"order {context.Saga.OrderId} was delivered successfully. this saga has ended");
                        context.Saga.OrderStatus = OrderStatus.DeliverySuccess;
                    })
                    .TransitionTo(OrderCompleteState)
                    .Finalize(),

                When(DeliveryFailureSagaEvent)
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
                       Items = context.Saga.Products,
                       Status = context.Saga.OrderStatus.Description
                   }))
                );


            SetCompletedWhenFinalized();
        }


        // 0 stage -> create order saga
        public Event<CreateOrderSagaEvent> CreateOrderSagaEvent { get; private set; }


        // 1st stage -> check if items are available to buy
        public Event<CatalogAllocationSuccessSagaEvent> CatalogAllocationSuccessSagaEvent { get; private set; }
        public Event<CatalogAllocationOutOfStockErrorSagaEvent> CatalogAllocationOutOfStockErrorSagaEvent { get; private set; }
        public Event<CatalogAllocationUnavailableErrorSagaEvent> CatalogAllocationUnavailableErrorSagaEvent { get; private set; }


        // 2nd stage -> payment process
        public Event<PaymentSuccessSagaEvent> PaymentSuccessSagaEvent { get; private set; }
        public Event<PaymentFailureSagaEvent> PaymentFailureSagaEvent { get; private set; }


        // 3rd stage -> tenant approves or rejects the order
        public Event<TenantApproveOrderSagaEvent> TenantApproveOrderSagaEvent { get; private set; }
        public Event<TenantRejectOrderSagaEvent> TenantRejectOrderSagaEvent { get; private set; }


        // 4th stage -> delivery
        public Event<DeliverySuccessSagaEvent> DeliverySuccessSagaEvent { get; private set; }
        public Event<DeliveryFailureSagaEvent> DeliveryFailureSagaEvent { get; private set; }



        public Event<CheckOrderStatusEvent> CheckOrderEvent { get; private set; }
    }
}
