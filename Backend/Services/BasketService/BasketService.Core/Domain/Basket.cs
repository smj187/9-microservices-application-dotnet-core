using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Core.Domain
{
    public class Basket : AggregateRoot
    {
        private List<Item> _items;
        private Guid? _userId;


        public Basket(Guid id)
        {
            Guard.Against.Null(id, nameof(id));
            Id = id;
            _items = new();
            _userId = null;

            CreatedAt = DateTimeOffset.UtcNow;
        }


        [JsonProperty]
        public Guid? UserId 
        { 
            get => _userId; 
            private set => _userId = value; 
        }

        public List<Item> Items 
        { 
            get => _items;
            private set => _items = value;
        }

        public int TotalItems
        {
            get => _items.Count;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach (var item in Items)
                {
                    total += item.Price * item.Quantity;
                }
                return total;
            }
        }

        public void AddItem(Item item)
        {
            Items.Add(item);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == itemId);
            Items.Remove(item);

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ClearItems()
        {
            _items.Clear();

            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void AssignUser(Guid? userId)
        {
            _userId = userId;
        }

    }
}
