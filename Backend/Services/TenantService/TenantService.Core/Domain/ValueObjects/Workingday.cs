using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Domain.Enumerations;

namespace TenantService.Core.Domain.ValueObjects
{
    public class Workingday : ValueObject
    {
        private string? _opening;
        private string? _closing;
        private Weekday _weekday;
        private string? _message;
        private bool _isClosedToday;

        // required by ef (never called)
        private Workingday() 
        {
            _opening = default!;
            _closing = default!;
            _weekday = default!;
        }

        public Workingday(Weekday weekday, string? message = null, bool? isClosedToday = true)
        {
            _opening = null;
            _closing = null;

            _weekday = weekday;
            _message = message;
            _isClosedToday = isClosedToday ?? true;
        }

        public Workingday(Weekday weekday, int? openingHour, int? closingHour, int? openingMinute = null, int? closingMinute = null)
        {
            _weekday = weekday;

            if (openingHour == null || closingHour == null)
            {
                throw new DomainViolationException("a workingday needs working hours");
            }

            _opening = String.Format("{0:00}:{1:00}", openingHour, openingMinute);
            _closing = String.Format("{0:00}:{1:00}", closingHour, closingMinute);

            _message = null;
            _isClosedToday = false;
        }

        public string? Opening 
        { 
            get => _opening;
            private set => _opening = value; 
        }

        public string? Closing
        {
            get => _closing;
            private set => _closing = value;
        }

        public Weekday Weekday
        {
            get => _weekday;
            private set => _weekday = value;
        }

        public string? Message
        {
            get => _message;
            private set => _message = value;
        }

        public bool IsClosedToday
        {
            get => _isClosedToday;
            private set => _isClosedToday = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Opening ?? null!;
            yield return Closing ?? null!;
            yield return Weekday;
            yield return Message ?? null!;
            yield return IsClosedToday;
        }
    }
}
