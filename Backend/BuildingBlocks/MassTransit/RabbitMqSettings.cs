using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.MassTransit
{
    public class RabbitMqSettings
    {
        public const string RabbitMqUri = "rabbitmq://localhost";
        public const string MediaCatalogQueue = "queue:media-catalog-queue";


        public const string Username = "guest";
        public const string Password = "guest";
    }
}
