using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Core.Domain
{
    public class Item : ValueObject
    {
        private Guid _id;
        private string _name;
        private string _image;
        private decimal _price;

        public Item(Guid id, string name, string image, decimal price)
        {
            Guard.Against.Null(id, nameof(id));
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(image, nameof(image));
            Guard.Against.NegativeOrZero(price, nameof(price));

            _id = id;
            _name = name;
            _image = image;
            _price = price;
        }

        public Guid Id
        { 
            get => _id;
            private set => _id = value; 
        }

        public string Name 
        {
            get => _name;
            private set => _name = value; 
        }

        public string Image
        {
            get => _image;
            private set => _image = value;
        }

        public decimal Price 
        { 
            get => _price; 
            private set => _price = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return Image;
            yield return Price;
        }
    }
}
