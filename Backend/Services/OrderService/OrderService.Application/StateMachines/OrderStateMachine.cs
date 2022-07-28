using BuildingBlocks.Masstransit;
using CatalogService.Contracts.v1.Commands;
using CatalogService.Contracts.v1.Events;
using DeliveryService.Contracts.v1.Commands;
using DeliveryService.Contracts.v1.Events;
using MassTransit;
using OrderService.Application.Consumers;
using OrderService.Application.StateMachines.Events;
using OrderService.Application.StateMachines.Responses;
using OrderService.Core.Entities.Enumerations;
using OrderService.Core.StateMachines;
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
        // saga
        public Event<InitializeOrderSagaEvent> InitializeOrderSagaEvent { get; private set; }
        public Event<CheckOrderStatusEvent> CheckOrderEvent { get; private set; }


        // catalog service
        public State AwaitingCatalogProductsAndSetsAllocationState { get; set; }
        public Event<CatalogProductsAndSetsAllocationSagaSuccessEvent> CatalogProductsAndSetsAllocationSagaSuccessEvent { get; private set; }
        public Event<CatalogProductsAndSetsOutOfStockSagaErrorEvent> CatalogProductsAndSetsOutOfStockSagaErrorEvent { get; private set; }
        public Event<CatalogProductsAndSetsUnavailableSagaErrorEvent> CatalogProductsAndSetsUnavailableSagaErrorEvent { get; private set; }


        // payment service
        public State AwaitingPaymentCompletionState { get; set; }
        public Event<PaymentProcessSagaSuccessEvent> PaymentProcessSagaSuccessEvent { get; private set; }
        public Event<PaymentProcessSagaFailureEvent> PaymentProcessSagaFailureEvent { get; private set; }


        // tenant service
        public State AwaitingTenantApprovalState { get; set; }
        public Event<TenantApproveOrderSagaEvent> TenantApproveOrderSagaEvent { get; private set; }
        public Event<TenantRejectOrderSagaEvent> TenantRejectOrderSagaEvent { get; private set; }


        // delivery service
        public State AwaitingDeliveryCompletionState { get; set; }
        public Event<DeliverySuccessSagaEvent> DeliverySuccessSagaEvent { get; private set; }
        public Event<DeliveryFailureSagaEvent> DeliveryFailureSagaEvent { get; private set; }

        


        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);
            Event(() => InitializeOrderSagaEvent, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => CheckOrderEvent, x =>
            {
                x.CorrelateById(context => context.Message.OrderId);

                x.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    await context.RespondAsync(new OrderNotFoundResponse
                    {
                        OrderId = context.Message.OrderId,
                        Message = $"no order with that id was found"
                    });
                }));
            });

            // catalog service
            Event(() => CatalogProductsAndSetsAllocationSagaSuccessEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => CatalogProductsAndSetsOutOfStockSagaErrorEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => CatalogProductsAndSetsUnavailableSagaErrorEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            // payment service
            Event(() => PaymentProcessSagaSuccessEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => PaymentProcessSagaFailureEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            // tenant service
            Event(() => TenantApproveOrderSagaEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => TenantRejectOrderSagaEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            // delivery service
            Event(() => DeliverySuccessSagaEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => DeliveryFailureSagaEvent, x => x.CorrelateById(context => context.Message.CorrelationId));


            Initially(
                When(InitializeOrderSagaEvent)
                    .Then(context =>
                    {
                        context.Saga.CorrelationId = context.Message.OrderId;
                        context.Saga.OrderId = context.Message.OrderId;

                        Console.WriteLine($"New Saga was created [{context.Message.TenantId}] '{context.Message.OrderId}'");

                        context.Saga.TenantId = context.Message.TenantId;
                        context.Saga.BasketId = context.Message.BasketId;
                        context.Saga.UserId = context.Message.UserId;
                        context.Saga.Products = context.Message.Products;
                        context.Saga.Sets = context.Message.Sets;

                        context.Saga.OrderStatus = OrderStatus.OrderCreatedStatus;

                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaCatalogConsumerEndpointName}"), context => context.Init<CatalogProductsAndSetsAllocationCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .TransitionTo(AwaitingCatalogProductsAndSetsAllocationState)
                );



            #region catalog service

            During(AwaitingCatalogProductsAndSetsAllocationState,
                When(CatalogProductsAndSetsAllocationSagaSuccessEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"products and sets are available.. continue");

                        context.Saga.TotalAmount = context.Message.Products.Sum(x => x.Price) + context.Message.Sets.Sum(x => x.Price);
                        context.Saga.OrderStatus = OrderStatus.CatalogProductsAndSetsAllocationSuccess;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaPaymentConsumerEndpointName}"), context => context.Init<PaymentProcessorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId, context.Saga.TotalAmount, context.Saga.UserId }))
                    .TransitionTo(AwaitingPaymentCompletionState),

                When(CatalogProductsAndSetsUnavailableSagaErrorEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderStatus = OrderStatus.CatalogProductsAndSetsUnavailableFailure;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaCatalogUnavailableErrorConsumerEndpointName}"), context => context.Init<HandleCatalogUnavailableSagaErrorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .Finalize(),

                When(CatalogProductsAndSetsOutOfStockSagaErrorEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderStatus = OrderStatus.CatalogProductsAndSetsOutOfStockFailure;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaCatalogOutOfStockErrorConsumerEndpointName}"), context => context.Init<HandleCatalogOutOfStockSagaErrorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .Finalize()
                );

            #endregion


            #region payment service

            During(AwaitingPaymentCompletionState,
                When(PaymentProcessSagaSuccessEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"payment successfully... continue");
                        context.Saga.OrderStatus = OrderStatus.PaymentProcessSuccess;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaTenantConsumerEndpointName}"), context => context.Init<TenantApprovalCommand>(new { context.Saga.CorrelationId, context.Saga.TenantId, context.Saga.Products, context.Saga.Sets, context.Saga.TotalAmount, context.Saga.UserId }))
                    .TransitionTo(AwaitingTenantApprovalState),

                When(PaymentProcessSagaFailureEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderStatus = OrderStatus.PaymentProcessFailure;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaPaymentFailureErrorConsumerEndpointName}"), context => context.Init<HandleCatalogUnavailableSagaErrorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .Finalize()
                );

            #endregion


            #region tenant service

            During(AwaitingTenantApprovalState,
                When(TenantApproveOrderSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"tenant approved order... continue");
                        context.Saga.OrderStatus = OrderStatus.TenantApproved;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaDeliveryConsumerEndpointName}"), context => context.Init<DeliveryProcessCommand>(new { context.Saga.UserId, context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .TransitionTo(AwaitingDeliveryCompletionState),

                When(TenantRejectOrderSagaEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderStatus = OrderStatus.TenantRejected;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaTenantRejectionErrorConsumerEndpointName}"), context => context.Init<HandleTenantRejectionSagaErrorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .Finalize()
                );

            #endregion


            #region delivery service

            During(AwaitingDeliveryCompletionState,
                When(DeliverySuccessSagaEvent)
                    .Then(context =>
                    {
                        Console.WriteLine($"order {context.Saga.CorrelationId} was delivered successfully");
                        context.Saga.OrderStatus = OrderStatus.DeliverySuccess;
                    })
                    .Finalize(),

                When(DeliveryFailureSagaEvent)
                    .Then(context =>
                    {
                        context.Saga.OrderStatus = OrderStatus.DeliveryFailed;
                    })
                    .SendAsync(new Uri($"queue:{RabbitMqSettings.OrderSagaTenantRejectionErrorConsumerEndpointName}"), context => context.Init<HandleDeliveryFailureSagaErrorCommand>(new { context.Saga.CorrelationId, context.Saga.Products, context.Saga.Sets, context.Saga.TenantId }))
                    .Finalize()
                );

            #endregion



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



    }
}
