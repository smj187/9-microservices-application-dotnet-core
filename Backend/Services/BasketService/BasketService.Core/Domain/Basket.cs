using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Core.Domain
{
    public class Basket : AggregateBase
    {
        private List<Item> _products;
        private List<Item> _sets;
        private Guid _userId;

        public Basket(Guid id, Guid userId)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.Null(userId, nameof(userId));

            _userId = userId;
            Id = id;
            _products = new();
            _sets = new();
        }

        public Guid UserId
        { 
            get => _userId; 
            private set => _userId = value; 
        }

        public List<Item> Products 
        { 
            get => _products;
            private set => _products = value;
        }

        public List<Item> Sets
        {
            get => _sets;
            private set => _sets = value;
        }

        public int TotalItems
        {
            get => _products.Count;
        }

        public decimal TotalPrice
        {
            get => _products.Sum(item => item.Price) + _sets.Sum(s => s.Price);
        }

        public void AddProduct(Item item)
        {
            Guard.Against.Null(item, nameof(item));
            _products.Add(item);

            Modify();
        }

        public void RemoveProduct(Guid itemId)
        {
            Guard.Against.Null(itemId, nameof(itemId));

            var product = _products.FirstOrDefault(i => i.Id == itemId);
            if(product != null)
            {
                _products.Remove(product);
                Modify();
            }
        }

        public void ClearCart()
        {
            _products.Clear();
            _sets.Clear();

            Modify();
        }

        public void AddSet(Item item)
        {
            Guard.Against.Null(item, nameof(item));
            _sets.Add(item);

            Modify();
        }

        public void RemoveSet(Guid itemId)
        {
            Guard.Against.Null(itemId, nameof(itemId));

            var set = _sets.FirstOrDefault(i => i.Id == itemId);
            if (set != null)
            {
                _sets.Remove(set);
                Modify();
            }
        }


    }
}
