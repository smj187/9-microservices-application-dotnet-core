using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Core.Domain.ValueObjects
{
    public class DeliveryStatus : Enumeration
    {
        public static DeliveryStatus Created = new(0, "Delivery created");

        public static DeliveryStatus Processing = new(1, "Delivery is being processed");
        public static DeliveryStatus Complete = new(2, "Delivery complete");

        public static DeliveryStatus Failed = new(2, "Delivery failed");

        public DeliveryStatus(int value, string description)
            : base(value, description)
        {

        }

        public static DeliveryStatus Create(int value)
        {
            if (value == 0) return new DeliveryStatus(Created.Value, Created.Description);

            if (value == 1) return new DeliveryStatus(Processing.Value, Processing.Description);
            if (value == 2) return new DeliveryStatus(Complete.Value, Complete.Description);

            if (value == 4) return new DeliveryStatus(Failed.Value, Failed.Description);

            throw new NotImplementedException();
        }
    }
}
