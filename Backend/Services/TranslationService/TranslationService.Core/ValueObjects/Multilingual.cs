using Ardalis.GuardClauses;
using BuildingBlocks.Domain;

namespace TranslationService.Core.ValueObjects
{
    public class Multilingual : ValueObject
    {
        private string _locale;
        private string _value;

        public Multilingual(string locale, string value)
        {
            Guard.Against.NullOrWhiteSpace(locale, nameof(locale));
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            _locale = locale;
            _value = value;
        }

        public string Locale
        { 
            get => _locale; 
            private set => _locale = value;
        }

        public string Value
        {
            get => _value;
            private set => _value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Locale;
            yield return Value;
        }
    }
}
