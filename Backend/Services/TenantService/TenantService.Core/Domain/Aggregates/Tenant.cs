using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions.Domain;
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
    public class Tenant : AggregateBase
    {
        private Address? _address;

        private List<Workingday> _workingdays;

        private string _name;
        private string? _description;
        private decimal _minimunOrderAmount;
        private bool _isFreeDelivery;
        private decimal? _deliveryCost;
        private string? _websiteUrl;
        private string? _imprint;
        private string _email;
        private string _phone;
        private string? _payments;

        private Guid? _brandImageAssetId;
        private Guid? _logoAssetId;
        private Guid? _videoAssetId;
        private Guid? _bannerAssetId;


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

        public Tenant(string name, Address address, string email, string phone)
        {
            Guard.Against.Null(address, nameof(address));

            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NullOrEmpty(email, nameof(email));
            Guard.Against.NullOrEmpty(phone, nameof(phone));

            _address = address;

            _name = name;
            _email = email;
            _phone = phone;


            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;
            _workingdays = new();

            _brandImageAssetId = null;
            _logoAssetId = null;
            _videoAssetId = null;
            _bannerAssetId = null;
        }



        // working days
        public List<Workingday> Workingdays
        {
            get => _workingdays;
            private set => _workingdays = value;
        }

        public void AddWorkingday(Workingday day)
        {
            var existing = _workingdays.FirstOrDefault(y => y.Weekday.Value == day.Weekday.Value);
            if (existing != null)
            {
                throw new DomainViolationException($"{day.Weekday.Description} ({day.Weekday.Value}) is already present");
            }

            _workingdays.Add(day);
            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void RemoveWorkingday(int weekday)
        {
            var existing = _workingdays.FirstOrDefault(x => x.Weekday.Value == weekday);
            if (existing == null)
            {
                throw new DomainViolationException($"{Weekday.Create(weekday).Description} is not in list");
            }

            _workingdays.Remove(existing);
            ModifiedAt = DateTimeOffset.UtcNow;
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



        // tenant information
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

        public string? Payments 
        { 
            get => _payments;
            private set => _payments = value; 
        }

        public void PatchInformation(string name, string? description, decimal minimunOrderAmount, bool isFreeDelivery, decimal? deliveryCost, string? websiteUrl, string? imprint, string email, string phone, string? payments)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NegativeOrZero(minimunOrderAmount, nameof(minimunOrderAmount));
            Guard.Against.Null(isFreeDelivery, nameof(isFreeDelivery));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(phone, nameof(phone));

            if (isFreeDelivery == false && deliveryCost == null)
            {
                throw new DomainViolationException("a delivery cost is required when offering free delivery");
            }

            _name = name;
            _description = description;
            _minimunOrderAmount = minimunOrderAmount;
            _isFreeDelivery = isFreeDelivery;
            _deliveryCost = deliveryCost;
            _websiteUrl = websiteUrl;
            _imprint = imprint;
            _email = email;
            _phone = phone;
            _payments = payments;


            ModifiedAt = DateTimeOffset.UtcNow;
        }




        // address
        public Address? Address
        {
            get => _address;
            private set => _address = value;
        }

        public void PatchAddress(Address address)
        {
            _address = address;
            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public void PatchCompanyDetails(string name, string? description)
        {
            _name = name;
            _description = description;
            ModifiedAt = DateTimeOffset.UtcNow;
        }


        // assets

        public Guid? BrandImageAssetId
        {
            get => _brandImageAssetId;
            private set => _brandImageAssetId = value;
        }

        public Guid? LogoAssetId
        {
            get => _logoAssetId;
            private set => _logoAssetId = value;
        }

        public Guid? VideoAssetId
        {
            get => _videoAssetId;
            private set => _videoAssetId = value;
        }

        public Guid? BannerAssetId
        {
            get => _bannerAssetId;
            private set => _bannerAssetId = value;
        }

        public void AddBrandImage(Guid imageId)
        {
            _brandImageAssetId = imageId;
        }

        public void AddLogo(Guid logoId)
        {
            _logoAssetId = logoId;
        }

        public void AddVideo(Guid videoId)
        {
            _videoAssetId = videoId;
        }

        public void AddBanner(Guid bannerId)
        {
            _bannerAssetId = bannerId;
        }

    }
}
