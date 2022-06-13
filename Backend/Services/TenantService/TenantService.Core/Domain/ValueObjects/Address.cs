using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService.Core.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zip;

        public Address(string street, string city, string state, string country, string zip)
        {
            Guard.Against.NullOrEmpty(street, nameof(street));
            Guard.Against.NullOrEmpty(city, nameof(city));
            Guard.Against.NullOrEmpty(state, nameof(state));
            Guard.Against.NullOrEmpty(country, nameof(country));
            Guard.Against.NullOrEmpty(zip, nameof(zip));

            _street = street;
            _city = city;
            _state = state;
            _country = country;
            _zip = zip;
        }


        public string Street
        {
            get => _street;
            private set => _street = value;
        }

        public string City
        {
            get => _city;
            private set => _city = value;
        }

        public string State
        {
            get => _state;
            private set => _state = value;
        }

        public string Country
        {
            get => _country;
            private set => _country = value;
        }

        public string Zip
        {
            get => _zip;
            private set => _zip = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return Zip;
        }
    }
}
