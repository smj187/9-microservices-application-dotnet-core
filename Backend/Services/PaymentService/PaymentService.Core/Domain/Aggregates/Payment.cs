using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Multitenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Core.Domain.Aggregates
{
    public class Payment : AggregateBase, IMultitenantAggregate
    {
        private string _tenantId;
        private Guid _userId;
        private Guid _orderId;
        private decimal _price;
        private List<Guid> _products;
        private List<Guid> _sets;


        // ef required (never called)
        protected Payment() 
        {
            _tenantId = default!;
            _products = default!;
            _sets = default!;
        }

        public Payment(string tenantId, Guid userId, Guid orderId, decimal price, List<Guid> products, List<Guid> sets)
        {
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.Null(userId, nameof(userId));
            Guard.Against.Null(orderId, nameof(orderId));
            Guard.Against.NegativeOrZero(price, nameof(price));
            Guard.Against.Null(sets, nameof(sets));

            _tenantId = tenantId;
            _userId = userId;
            _orderId = orderId;
            _price = price;
            _products = products;
            _sets = sets;

            CreatedAt = DateTimeOffset.UtcNow;
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

        public Guid OrderId
        {
            get => _orderId;
            private set => _orderId = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public List<Guid> Products
        {
            get => _products;
            private set => _products = value;
        }

        public  List<Guid> Sets
        {
            get => _sets;
            private set => _sets = value;
        }
    }
}
