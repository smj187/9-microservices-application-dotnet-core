using Ardalis.GuardClauses;
using BuildingBlocks.Domain;
using BuildingBlocks.Exceptions.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.ValueObjects;

namespace TranslationService.Core.Aggregates
{
    public class Translation : AggregateBase
    {
        private string _tenantId;
        private string _service;
        private string _resource;
        private string _identifier;
        private string _field;
        private List<Multilingual> _multilinguals;
        private string _key;

        public Translation(string tenantId, string service, string resource, string identifier, string field, List<Multilingual> multilinguals)
        {
            Guard.Against.NullOrWhiteSpace(tenantId, nameof(tenantId));
            Guard.Against.NullOrWhiteSpace(service, nameof(service));
            Guard.Against.NullOrWhiteSpace(resource, nameof(resource));
            Guard.Against.NullOrWhiteSpace(identifier, nameof(identifier));
            Guard.Against.NullOrWhiteSpace(field, nameof(field));
            Guard.Against.NullOrEmpty(multilinguals, nameof(multilinguals));

            _tenantId = tenantId;
            _service = service;
            _resource = resource;
            _identifier = identifier;
            _field = field;
            _multilinguals = multilinguals;

            _key = $"{tenantId}_{service}_{resource}_{identifier}_{field}";
        }


        public string TenantId
        {
            get => _tenantId; 
            private set => _tenantId = value;
        }

        public string Service
        {
            get => _service;
            private set => _service = value;
        }

        public string Resource
        {
            get => _resource;
            private set => _resource = value;
        }

        public string Identifier
        {
            get => _identifier;
            private set => _identifier = value;
        }

        public string Field
        {
            get => _field;
            private set => _field = value;
        }

        public List<Multilingual> Multilinguals
        {
            get => _multilinguals;
            private set => _multilinguals = value;
        }

        public string Key
        {
            get => _key;
            private set => _key = value;
        }

        public void AddMultilingual(string locale, string value)
        {
            Guard.Against.NullOrWhiteSpace(locale, nameof(locale));
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            var existing = _multilinguals.FirstOrDefault(m => m.Locale == locale);
            if (existing != null)
            {
                _multilinguals.Remove(existing);
            }

            var multilingual = new Multilingual(locale, value);
            _multilinguals.Add(multilingual);
        }

        public void RemoveMultilingual(string locale)
        {
            Guard.Against.NullOrWhiteSpace(locale, nameof(locale));

            var multilingual = _multilinguals.FirstOrDefault(m => m.Locale == locale);
            if (multilingual != null)
            {
                _multilinguals.Remove(multilingual);
            }
        }

        public void PatchMultilingual(string locale, string value)
        {
            Guard.Against.NullOrWhiteSpace(locale, nameof(locale));
            Guard.Against.NullOrWhiteSpace(value, nameof(value));

            var existing = _multilinguals.FirstOrDefault(m => m.Locale == locale);
            if (existing == null)
            {
                throw new DomainViolationException($"the locale {locale} is not present");
            }

            _multilinguals.Remove(existing);
            var multilingual = new Multilingual(locale, value);
            _multilinguals.Add(multilingual);
        }

    }
}
