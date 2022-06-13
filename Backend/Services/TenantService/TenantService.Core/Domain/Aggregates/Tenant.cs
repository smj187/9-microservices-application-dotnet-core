using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions;
using BuildingBlocks.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain;
using TenantService.Core.Domain.Enumerations;
using TenantService.Core.Domain.ValueObjects;

namespace TenantService.Core.Domain.Aggregates
{
    public class Tenant : AggregateRoot
    {
        private string _name;
        private string? _description;
        private Address? _address;
        private string _email;
        private string _phone;
        private List<Workingday> _workingdays;

        private decimal _minimunOrderAmount;
        private bool _isFreeDelivery;
        private decimal? _deliveryCost;
        private string? _websiteUrl;
        private string? _imprint;


        // ef required (never called)
        public Tenant()
        {
            _name = default!;
            _description = null!;
            _address = default!;
            _workingdays = default!;
            _email = default!;
            _phone = default!;
        }

        public Tenant(string name, Address address, string email, string phone, string? description, decimal minimunOrderAmount, bool isFreeDelivery, decimal? deliveryCost, string? websiteUrl, string? imprint)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.Null(address, nameof(address));
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(phone, nameof(phone));
            Guard.Against.NullOrNegativ(minimunOrderAmount, nameof(minimunOrderAmount));
            Guard.Against.Null(isFreeDelivery, nameof(isFreeDelivery));

            _name = name;
            _description = description;
            _address = address;

            _minimunOrderAmount = minimunOrderAmount;
            _isFreeDelivery = isFreeDelivery;
            _deliveryCost = deliveryCost;
            _websiteUrl = websiteUrl;
            _imprint = imprint;

            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            if (isFreeDelivery == false && deliveryCost == null)
            {
                throw new DomainViolationException("a delivery cost is required when offering free delivery");
            }


            _email = email;
            _phone = phone;

            _workingdays = new();
            //_workingdays = new List<Workingday>
            //{
            //    new Workingday(Weekday.Create(0), 11, 22, 30, 0),
            //    new Workingday(Weekday.Create(1)),
            //    new Workingday(Weekday.Create(6), 11, 23, 50, 0),
            //};
        }


        // contact
        public string Email
        {
            get => _email;
            private set => _email = value;
        }

        public string Phone
        {
            get => _phone;
            private set => _phone = value;
        }



        // working days
        public List<Workingday> Workingdays
        {
            get => _workingdays;
            private set => _workingdays = value;
        }

        public void AddWorkingday(Workingday day)
        {
            _workingdays.Add(day);
            //_workingdays = new List<Workingday>
            //{
            //    new Workingday(Weekday.Create(0), 11, 22, 30, 0),
            //    new Workingday(Weekday.Create(1)),
            //    new Workingday(Weekday.Create(6), 11, 23, 50, 0),
            //};
        }

        public void RemoveWorkingday(int weekday)
        {
            var day = _workingdays.FirstOrDefault(x => x.Weekday.Value == weekday);
            _workingdays.Remove(day);
        }

        public bool IsOpen
        {
            get
            {
                var existingDay = _workingdays.FirstOrDefault(x => x.Weekday.Description == DateTimeOffset.UtcNow.DayOfWeek.ToString());

                if (existingDay != null && existingDay.Opening != null && existingDay.Closing != null)
                {
                    var start = DateTimeOffset.ParseExact(existingDay.Opening, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                    var end = DateTimeOffset.ParseExact(existingDay.Closing, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;

                    return (DateTimeOffset.UtcNow.TimeOfDay > start) && (DateTimeOffset.UtcNow.TimeOfDay < end);
                }

                return false;
            }
        }



        // company specifics
        public decimal MinimunOrderAmount 
        { 
            get => _minimunOrderAmount;
            private set => _minimunOrderAmount = value; 
        }

        public bool IsFreeDelivery
        {
            get => _isFreeDelivery;
            private set => _isFreeDelivery = value;
        }

        public decimal? DeliveryCost
        {
            get => _deliveryCost;
            private set => _deliveryCost = value;
        }

        public string? WebsiteUrl
        {
            get => _websiteUrl;
            private set => _websiteUrl = value;
        }

        public string? Imprint
        {
            get => _imprint;
            private set => _imprint = value;
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        // TODO: payment options


        // address
        public Address? Address
        {
            get => _address;
            private set => _address = value;
        }
        public void RemoveAddress()
        {
            _address = null;
        }

        public void PatchAddress(Address address)
        {
            _address = address;
        }


        public void PatchDescription(string name, string? description)
        {
            _name = name;
            _description = description;
        }
    }
}
