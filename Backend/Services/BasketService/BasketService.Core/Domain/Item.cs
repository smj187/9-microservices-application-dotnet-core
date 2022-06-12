using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Core.Domain
{
    public class Item : ValueObject
    {
        private Guid _itemId;
        private string _itemName;
        private string _itemImage;
        private decimal _price;
        private int _quantity;

        public Item(Guid itemId, string itemName, string itemImage, decimal price, int quantity)
        {
            Guard.Against.Null(itemId, nameof(itemId));
            Guard.Against.NullOrWhiteSpace(itemName, nameof(itemName));
            Guard.Against.NullOrWhiteSpace(itemImage, nameof(itemImage));
            Guard.Against.NullOrNegativ(price, nameof(price));
            Guard.Against.NullOrNegativ(quantity, nameof(quantity));

            _itemId = itemId;
            _itemName = itemName;
            _itemImage = itemImage;
            _price = price;
            _quantity = quantity;
        }

        public Guid ItemId
        { 
            get => _itemId;
            private set => _itemId = value; 
        }

        public string ItemName 
        {
            get => _itemName;
            private set => _itemName = value; 
        }

        public string ItemImage
        {
            get => _itemImage;
            private set => _itemImage = value;
        }

        public decimal Price 
        { 
            get => _price; 
            private set => _price = value;
        }

        public int Quantity 
        {
            get => _quantity;
            private set => _quantity = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ItemId;
            yield return ItemName;
            yield return ItemImage;
            yield return Price;
            yield return Quantity;
        }
    }
}
