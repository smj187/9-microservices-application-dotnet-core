using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using OrderService.Core.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Core.Entities.Aggregates
{
    public class Order : AggregateBase, IMultitenantAggregate
    {
        private string _tenantId;
        private Guid _userId;
        private List<Guid> _products;
        private List<Guid> _sets;
        private int _orderStatusValue;
        private string _orderStatusDescription;
        private decimal _totalAmount;

        public Order(string tenantId, Guid orderId, Guid userId, List<Guid> products, List<Guid> sets)
        {
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.Null(orderId, nameof(orderId));
            Guard.Against.Null(userId, nameof(userId));
            Guard.Against.Null(products, nameof(products));
            Guard.Against.Null(sets, nameof(sets));

            Id = orderId;

            _tenantId = tenantId;
            _userId = userId;
            _products = products;
            _sets = sets;
            _totalAmount = 0;

            var created = OrderStatus.Create(0);
            _orderStatusDescription = created.Description;
            _orderStatusValue = created.Value;

        }

        public string TenantId
        {
            get => _tenantId;
            set => _tenantId = Guard.Against.NullOrWhiteSpace(value, nameof(value));
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

        public decimal TotalAmount
        {
            get => _totalAmount; 
            private set => _totalAmount = value;
        }

        public int OrderStatusValue
        {
            get => _orderStatusValue;
            private set => _orderStatusValue = value;
        }

        public string OrderStatusDescription
        {
            get => _orderStatusDescription;
            private set => _orderStatusDescription = value;
        }

        public void ChangeOrderStatus(OrderStatus orderStatus)
        {
            Guard.Against.Null(orderStatus, nameof(orderStatus));

            _orderStatusDescription = orderStatus.Description;
            _orderStatusValue = orderStatus.Value;

            Modify();
        }

        public void ChangeTotalAmount(decimal totalAmount)
        {
            Guard.Against.Negative(totalAmount, nameof(totalAmount));

            _totalAmount = totalAmount;

            Modify();
        }
    }
}
