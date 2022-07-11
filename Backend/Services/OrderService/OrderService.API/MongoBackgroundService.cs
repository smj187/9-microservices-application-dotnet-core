using BuildingBlocks.Exceptions.Domain;
using BuildingBlocks.Multitenancy.Services;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using OrderService.Application.Commands;
using OrderService.Core.Entities.Aggregates;
using OrderService.Core.StateMachines;
using OrderService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.API
{
    public class MongoBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public MongoBackgroundService(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        private async Task HandleOrderCreation(IMongoCollection<OrderStateMachineInstance> collection, string filter)
        {
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<OrderStateMachineInstance>>().Match(filter);
            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            using var cursor = await collection.WatchAsync(pipeline, options);

            while (await cursor.MoveNextAsync())
            {
                foreach (var info in cursor.Current)
                {
                    var command = new CreateOrderCommand
                    {
                        TenantId = info.FullDocument.TenantId,
                        OrderId = info.FullDocument.OrderId,
                        Products = info.FullDocument.Products,
                        Sets = info.FullDocument.Sets,
                        UserId = info.FullDocument.UserId
                    };

                    await _mediator.Send(command);
                }
            }
        }

        private async Task HandleOrderUpdate(IMongoCollection<OrderStateMachineInstance> collection, string filter)
        {
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<OrderStateMachineInstance>>().Match(filter);
            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            using var cursor = await collection.WatchAsync(pipeline, options);

            while (await cursor.MoveNextAsync())
            {
                foreach (var info in cursor.Current)
                {
                    var command = new UpdateOrderCommand
                    {
                        TenantId = info.FullDocument.TenantId,
                        OrderId = info.FullDocument.OrderId,
                        TotalAmount = info.FullDocument.TotalAmount,
                        OrderStatus = info.FullDocument.OrderStatus,
                    }; 
                    
                    await _mediator.Send(command);
                }
            }
        }

        private async Task HandleOrderCompletion(IMongoCollection<OrderStateMachineInstance> collection, string filter)
        {
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<OrderStateMachineInstance>>().Match(filter);
            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            using var cursor = await collection.WatchAsync(pipeline, options);

            while (await cursor.MoveNextAsync())
            {
                foreach (var info in cursor.Current)
                {
                    if (info.FullDocument != null)
                    {
                        var command = new CompleteOrderCommand
                        {
                            OrderId = info.FullDocument.OrderId,
                            TenantId = info.FullDocument.TenantId
                        };

                        await _mediator.Send(command);
                    }

                    var x = 1;

                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("... MongoBackgroundService ...");
            Console.WriteLine("... MongoBackgroundService ...");
            Console.WriteLine("... MongoBackgroundService ...");

            var mongoClient = new MongoClient(_configuration.GetSection("SagaPersistence:ConnectionString").Value);
            var sandboxDB = mongoClient.GetDatabase(_configuration.GetSection("SagaPersistence:DatabaseName").Value);
            var collection = sandboxDB.GetCollection<OrderStateMachineInstance>(_configuration.GetSection("SagaPersistence:CollectionName").Value);


            Parallel.Invoke(
                async () => await HandleOrderCreation(collection, "{ operationType: { $in: [ 'insert' ] } }"),
                async () => await HandleOrderUpdate(collection, "{ operationType: { $in: [ 'replace' ] } }"),
                async () => await HandleOrderCompletion(collection, "{ operationType: { $in: [ 'delete' ] } }")
            );

            

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(4000, stoppingToken);
            }
        }
    }
}
