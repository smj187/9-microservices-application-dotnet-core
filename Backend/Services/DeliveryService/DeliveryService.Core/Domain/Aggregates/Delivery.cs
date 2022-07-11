using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using DeliveryService.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Core.Domain.Aggregates
{
    public class Delivery : AggregateBase, IMultitenantAggregate
    {
        private string _tenantId;
        private Guid _orderId;
        private Guid _userId;
        private List<Guid> _products;
        private List<Guid> _sets;
        private int _deliveryStatusValue;
        private string _deliveryStatusDescription;

        public Delivery(string tenantId, Guid orderId, Guid userId, List<Guid> products, List<Guid> sets)
        {
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.Null(orderId, nameof(orderId));
            Guard.Against.Null(userId, nameof(userId));
            Guard.Against.Null(products, nameof(products));
            Guard.Against.Null(sets, nameof(sets));

            _tenantId = tenantId;
            _orderId = orderId;
            _userId = userId;
            _products = products;
            _sets = sets;

            var created = DeliveryStatus.Create(0);
            _deliveryStatusValue = created.Value;
            _deliveryStatusDescription = created.Description;
        }

        public string TenantId
        {
            get => _tenantId;
            set => _tenantId = Guard.Against.NullOrWhiteSpace(value, nameof(value));
        }

        public Guid OrderId
        {
            get => _orderId;
            private set => _orderId = value;
        }

        public Guid UserId
        {
            get => _userId;
            private set => _userId = value;
        }

        public List<Guid> Products
        {
            get => _products;
            private set => _products = value;
        }

        public List<Guid> Sets
        {
            get => _sets;
            private set => _sets = value;
        }

        public int DeliveryStatusValue
        {
            get => _deliveryStatusValue;
            private set => _deliveryStatusValue = value;
        }
        
        public string DeliveryStatusDescription
        {
            get => _deliveryStatusDescription;
            private set => _deliveryStatusDescription = value;
        }

        public void ChangeDeliveryStatus(DeliveryStatus deliveryStatus)
        {
            Guard.Against.Null(deliveryStatus, nameof(deliveryStatus));

            _deliveryStatusValue = deliveryStatus.Value;
            _deliveryStatusDescription = deliveryStatus.Description;

            Modify();
        }
    }
}
